using gs_loader_common.Base;
using gs_loader_common.Forms;
using gs_loader_common.Setup;
using gs_loader_common.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace gs_loader_setup
{
    public partial class MainForm : Form
    {
        private string _lastSelectedPath = Directory.GetCurrentDirectory();

        SetupData setupData = new SetupData();
        //List<SetupFileItem> setupFiles = new List<SetupFileItem>();

        string SetupPath = "";
        string SetupFile = "";

        public MainForm(SetupData setupData)
        {
            InitializeComponent();

            ignoreExts.DefaultValues = SetupData.DefaultIgnoredExtensions;
            includeExt.DefaultValues = SetupData.DefaultExtensions;

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

            if (cmbExecutable.SelectedItem == null)
            {
                Dialog.Error("Você não selecionou um executável para o setup!");
                return;
            }
            if (!Dialog.YesNo("Deseja gravar o arquivo de setup em " + lblOriginFolder.Text + "?"))
                return;
            setupData.Arguments = txArguments.Text;
            setupData.IgnoredExtensions = ignoreExts.Value;
            setupData.IncludeExtensions = includeExt.Value;
            setupData.JustOneInstance = chkJustOneInstance.Checked;
            setupData.UpdateSource = (UpdateSource)editUpdateSource.UpdateSource.Clone();
            setupData.UpdateType = editUpdateSource.UpdateType;

            if (SetupData.Write(lblOriginFolder.Text, setupData, out string message))
            {
                Dialog.Message("Dados gravados em " + lblOriginFolder.Text + "\n\n" + message);
            }
            else
            {
                Dialog.Error("Erro ao gravar em " + lblOriginFolder.Text + "\n\n" + message);
            }

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
            includeExt.Value = setupData.IncludeExtensions;
            ignoreExts.Value = setupData.IgnoredExtensions;
            #endregion

            setupData.Files.AddFilesFromFolder(setupFolder);
            dgvFiles.RowCount = setupData.Files.Count;
        }

        private void DGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= setupData.Files.Count)
                return;
            switch (e.ColumnIndex)
            {
                case 5:
                    setupData.Files[e.RowIndex].Include = !setupData.Files[e.RowIndex].Include;
                    break;
                case 6:
                    setupData.Files[e.RowIndex].Executable = !setupData.Files[e.RowIndex].Executable;

                    if (setupData.Files[e.RowIndex].Executable)
                    {
                        if (!IO.IsExecutable(setupData.Files[e.RowIndex].File))
                        {
                            setupData.Files[e.RowIndex].Executable = false;
                            Dialog.Error("O arquivo " + setupData.Files[e.RowIndex].File + " não é do tipo executável!");
                        }
                        else
                            for (int i = 0; i < setupData.Files.Count; i++)
                                if (i != e.RowIndex)
                                    setupData.Files[i].Executable = false;
                        dgvFiles.Refresh();
                    }
                    break;
            }

        }

        private void DGB_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= setupData.Files.Count)
                return;
            SetupFile setupFileItem = setupData.Files[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0: // Arquivo local
                    e.Value = Path.GetFileName(setupFileItem.File);
                    break;
                case 1: // Pasta
                    e.Value = setupFileItem.Folder;
                    break;
                case 2: // Estado
                    e.Value = setupFileItem.State;
                    break;
                case 3: // Tamanho
                    e.Value = setupFileItem.Size;
                    break;
                case 4: // MD-5
                    e.Value = setupFileItem.MD5;
                    break;
                case 5: // Incluir
                    e.Value = setupFileItem.Include;
                    break;
                case 6: // Executável
                    e.Value = setupFileItem.Executable;
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

        private void DGVFiles_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvFiles.IsCurrentCellDirty)
            {
                dgvFiles.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
