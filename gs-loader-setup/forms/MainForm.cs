using gs_loader_common.Base;
using gs_loader_common.Forms;
using gs_loader_common.Setup;
using gs_loader_common.Update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gs_loader_setup
{
    public partial class MainForm : Form
    {
        private string _lastSelectedPath = Directory.GetCurrentDirectory();

        List<SetupFileItem> setupFiles = new List<SetupFileItem>();

        string SetupPath = "";
        string SetupFile = "";

        public MainForm(SetupData setupData)
        {
            InitializeComponent();

            if (setupData == null)  // Sem arquivo de setup informado
                return;

            Read(setupData);

        }
        private void ArquivoNovo()
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "Novo arquivo de setup",
                RootFolder = Environment.SpecialFolder.MyComputer,
                SelectedPath = _lastSelectedPath,
                ShowNewFolderButton = false
            })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    WaitForm.Mostrar("Carregando informações de\n" + fbd.SelectedPath);
                    if (SetupData.Read(fbd.SelectedPath, out SetupData setupData, out string message))
                    {
                        Dialog.Message("Já existe um arquivo em " + setupData.SetupFile + ".\nEle será carregado.");
                        Read(setupData);
                    }
                    else
                    {
                        if (SetupData.Create(fbd.SelectedPath, out setupData, out message))
                        {
                            Read(setupData);
                            SetupPath = fbd.SelectedPath;
                            SetupFile = setupData.SetupFile;
                        }
                        else
                        {
                            Dialog.Error("Não foi possível criar setup em " + fbd.SelectedPath + "\n\n" + message);
                        }
                    }
                    WaitForm.Fechar();
                }
            }

        }

        private void MenuArquivoClick(object sender, EventArgs e)
        {
            if (sender == miArquivoNovo)
            {
                //TODO: Criar arquivo de setup em uma pasta
                ArquivoNovo();

            }
            else if (sender == miArquivoAbrir)
            {
                //TODO: Abrir arquivo de setup em uma pasta
            }
            else if (sender == miArquivoSalvar)
            {
                ArquivoGravar();
                //TODO: Salvar arquivo de setup
            }
        }

        private void ArquivoGravar()
        {
            if (!Dialog.YesNo("Deseja gravar o arquivo de setup em " + lblOriginFolder.Text + "?"))
                return;
            SetupData setup = new SetupData()
            {
                Executable = new SetupFile(cmbExecutable.SelectedValue.ToString()),
                Arguments = txArguments.Text,
                IgnoredExtensions = ignoreExts.Value,
                IncludeExtensions = includeExts.Value,
                JustOneInstance = chkJustOneInstance.Checked,
                UpdateSource = (UpdateSource)editUpdateSource.UpdateSource.Clone(),
                UpdateType = editUpdateSource.UpdateType,
                Files = new List<SetupFile>()
            };
            foreach (var f in setupFiles)
            {
                if (f.Include)
                    setup.Files.Add(f.SetupFile);
            }
            if (SetupData.Write(lblOriginFolder.Text, setup, out string message))
            {
                Dialog.Message("Dados gravados em " + lblOriginFolder.Text + "\n\n" + message);
            }
            else
            {
                Dialog.Error("Erro ao gravar em " + lblOriginFolder.Text + "\n\n" + message);
            }

        }

        private void ParseFiles(string setupPath, List<SetupFile> files)
        {
            setupFiles.Clear();

            var f = Directory.GetFiles(setupPath, "*.*", SearchOption.AllDirectories).ToList();

            // Remove arquivos com extensões ignoradas
            int i = 0;
            while (i < f.Count())
            {
                string ext = Path.GetExtension(f[i]);
                if (Extensions.Contains(ignoreExts.Value, ext))
                    f.RemoveAt(i);
                else i++;
            }

            // Adiciona arquivos existentes na pasta
            for (i = 0; i < f.Count; i++)
            {
                SetupFileState fileState = SetupFileState.OnFolderUnselected;
                // Verifica se a extensão não faz parte da lista de extensões requeridas
                if (Extensions.Contains(includeExts.Value, Path.GetExtension(f[i])))
                    fileState = SetupFileState.OnFolder;

                setupFiles.Add(new SetupFileItem
                {
                    FileName = f[i],
                    State = fileState,
                    SetupFile = new SetupFile(f[i], setupPath),
                    Include = fileState == SetupFileState.OnFolder
                });
            }

            // Adiciona arquivos do setup
         /*   foreach (var file in files)
            {
                foreach (var fileItem in setupFiles)
                {
                    if (fileItem.FileName)
                }
            }
            */
            for (var iFiles = 0; iFiles < files.Count; iFiles++)
            {
                int k = -1;
                for (int j = 0; j < setupFiles.Count; j++)
                {
                    string setupFileName = setupFiles[j].FileName.Substring(setupPath.Length+1);
                    //DONE: Revisar o loop em files[i] causando exceção
                    if (setupFileName.Equals(Path.Combine(files[iFiles].Folder, files[iFiles].File), StringComparison.InvariantCultureIgnoreCase))
                    {
                        k = j;
                        break;
                    }
                }
                if (k == -1)
                {
                    // Não existe na lista atual
                    setupFiles.Add(new SetupFileItem
                    {
                        FileName = Path.Combine(setupPath, files[iFiles].Folder, files[iFiles].File),
                        SetupFile = new SetupFile(Path.Combine(setupPath, files[iFiles].Folder, files[iFiles].File)),
                        State = SetupFileState.OnSetup
                    });
                }
                else
                {
                    // Atualiza a lista
                    setupFiles[k].State = SetupFileState.Synced;
                    setupFiles[k].Include = true;
                }
            }

            // Ordena a lista
            //setupFiles.Sort((x, y) => x.FileName.CompareTo(y.FileName));

            setupFiles.Sort((x, y) => x.CompareTo(y));

            dgvFiles.RowCount = setupFiles.Count;
        }

        private void Read(SetupData setupData)
        {
            Text = "GS-Loader Setup: " +
                (string.IsNullOrEmpty(setupData.SetupFile) ? "Sem arquivo" : setupData.SetupFile) +
                (!File.Exists(setupData.SetupFile) ? " [inexistente]" : "");
            miArquivoSalvar.Enabled = true;
            string setupFolder = Path.GetDirectoryName(setupData.SetupFile);
            lblOriginFolder.Text = setupFolder;

            #region Executavel
            // Obter lista de executáveis da pasta
            cmbExecutable.Items.Clear();
            var exes = Directory.GetFiles(setupFolder, "*.exe");
            int iExe = -1;
            foreach (var exe in exes)
            {
                cmbExecutable.Items.Add(Path.GetFileName(exe));
                if (Path.GetFileName(exe).Equals(setupData.Executable.File, StringComparison.InvariantCultureIgnoreCase))
                    iExe = cmbExecutable.Items.Count - 1;
            }
            cmbExecutable.SelectedIndex = iExe;

            chkJustOneInstance.Checked = setupData.JustOneInstance;
            txArguments.Text = setupData.Arguments;
            #endregion

            #region Atualizacao
            editUpdateSource.UpdateSource = setupData.UpdateSource;
            editUpdateSource.UpdateType = setupData.UpdateType;
            #endregion

            #region Extensões
            includeExts.Value = setupData.IncludeExtensions;
            ignoreExts.Value = setupData.IgnoredExtensions;
            #endregion

            ParseFiles(setupFolder, setupData.Files);
        }

        private void DGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= setupFiles.Count || e.ColumnIndex != 5)
                return;
            setupFiles[e.RowIndex].Include = !setupFiles[e.RowIndex].Include;
        }

        private void DGB_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= setupFiles.Count)
                return;
            SetupFileItem setupFileItem = setupFiles[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0: // Arquivo local
                    e.Value = Path.GetFileName(setupFileItem.FileName);
                    break;
                case 1: // Pasta
                    e.Value = setupFileItem.SetupFile == null ? "" : setupFileItem.SetupFile.Folder;
                    break;
                case 2: // Estado
                    e.Value = setupFileItem.State;
                    break;
                case 3: // Tamanho
                    e.Value = setupFileItem.SetupFile.Size;
                    break;
                case 4: // MD-5
                    e.Value = setupFileItem.SetupFile.MD5;
                    break;
                case 5: // Incluir
                    e.Value = setupFileItem.Include;
                    break;
            }
        }

        private void MenuDistribuicaoClick(object sender, EventArgs e)
        {
            if (sender == miDistribuicaoGerar)
            {
                DistribuicaoGerar();
            }
        }

        private void DistribuicaoGerar()
        {
            //TODO: Gerar arquivos de distribuição
            throw new NotImplementedException();
        }
    }
}
