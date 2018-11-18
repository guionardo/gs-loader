using gs_loader_common.Base;
using gs_loader_common.Forms;
using gs_loader_common.Setup;
using gs_loader_common.Update;

using System;
using System.IO;
using System.Windows.Forms;

namespace gs_loader_setup
{
    public partial class MainForm : Form
    {
        private string _lastSelectedPath = Directory.GetCurrentDirectory();

        SetupData formSetupData = new SetupData();

        string SetupFile = "";
        string SetupPath = "";
        public MainForm(SetupData setupData)
        {
            InitializeComponent();

            ignoreExts.DefaultValues = SetupData.DefaultIgnoredExtensions;
            includeExt.DefaultValues = SetupData.DefaultExtensions;

            LoadLastFiles();
            if (setupData == null)  // Sem arquivo de setup informado
                return;
            this.formSetupData = setupData;
            Read(this.formSetupData);

        }

        private void ArquivoGravar()
        {
            bool HasExecutable = false;
            bool HasInclude = false;
            foreach (var f in formSetupData.Files)
            {
                if (f.FileFlags.HasFlag(SetupFileFlags.MainExecutable))
                    HasExecutable = true;
                if (f.FileFlags.HasFlag(SetupFileFlags.Include))
                    HasInclude = true;
                if (HasExecutable && HasInclude)
                    break;
            }
            if (!HasExecutable)
            {
                string probExecutable = IO.FindMainExecutable(lblOriginFolder.Text);
                if (!string.IsNullOrEmpty(probExecutable))
                {
                    probExecutable = Path.GetFileName(probExecutable);
                    foreach (var f in formSetupData.Files)
                        if (string.IsNullOrEmpty(f.Folder) && Path.GetFileName(f.File).Equals(probExecutable, StringComparison.InvariantCultureIgnoreCase))
                        {
                            f.FileFlags |= SetupFileFlags.Include | SetupFileFlags.MainExecutable;
                            HasExecutable = true;
                            HasInclude = true;
                            break;
                        }
                }
                if (!HasExecutable)
                {
                    Dialog.Error("Você não selecionou um executável para o setup!");
                    return;
                }
            }
            if (!HasInclude)
            {
                Dialog.Error("Você não selecionou nenhum arquivo para o setup!");
                return;
            }
            if (!Dialog.YesNo("Deseja gravar o arquivo de setup em " + lblOriginFolder.Text + "?"))
                return;
            formSetupData.Arguments = txArguments.Text;
            formSetupData.IgnoredExtensions = ignoreExts.Value;
            formSetupData.IncludeExtensions = includeExt.Value;
            formSetupData.JustOneInstance = chkJustOneInstance.Checked;
            formSetupData.UpdateSource = (UpdateSource)editUpdateSource.UpdateSource.Clone();
            formSetupData.UpdateType = editUpdateSource.UpdateType;

            if (SetupData.Write(lblOriginFolder.Text, formSetupData, out string message))
            {
                Dialog.Message("Dados gravados em " + lblOriginFolder.Text + "\n\n" + message);
                Config.AddLastFile(formSetupData.SetupFile);
            }
            else
            {
                Dialog.Error("Erro ao gravar em " + lblOriginFolder.Text + "\n\n" + message);
            }

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
                        if (SetupData.Create(fbd.SelectedPath, out formSetupData, out message))
                        {
                            Read(this.formSetupData);
                            SetupPath = fbd.SelectedPath;
                            SetupFile = formSetupData.SetupFile;
                            Config.AddLastFile(SetupFile);
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

        private void DGB_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (formSetupData == null || e.RowIndex < 0 || e.RowIndex >= formSetupData.Files.Count)
                return;
            SetupFile setupFileItem = formSetupData.Files[e.RowIndex];
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
                    e.Value = setupFileItem.FileFlags.HasFlag(SetupFileFlags.Include);
                    break;
                case 6: // Executável
                    e.Value = setupFileItem.FileFlags.HasFlag(SetupFileFlags.MainExecutable);
                    break;
            }
        }

        private void DGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= formSetupData.Files.Count)
                return;
            SetupFile sf = formSetupData.Files[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 5:
                    sf.FileFlags.Invert(SetupFileFlags.Include);
                    break;
                case 6:
                    sf.FileFlags.Invert(SetupFileFlags.MainExecutable);

                    if (sf.FileFlags.HasFlag(SetupFileFlags.MainExecutable))
                    {
                        if (!IO.IsExecutable(sf.File))
                        {
                            sf.FileFlags.Remove(SetupFileFlags.MainExecutable);
                            Dialog.Error("O arquivo " + formSetupData.Files[e.RowIndex].File + " não é do tipo executável!");
                        }
                        else
                            for (int i = 0; i < formSetupData.Files.Count; i++)
                                if (i != e.RowIndex)
                                    formSetupData.Files[i].FileFlags.Remove(SetupFileFlags.MainExecutable);
                        dgvFiles.Invalidate();
                    }
                    break;
            }

        }

        private void DGVFiles_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvFiles.IsCurrentCellDirty)
            {
                dgvFiles.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DistribuicaoGerar()
        {
            //TODO: Gerar arquivos de distribuição
            throw new NotImplementedException();
        }

        private void LoadLastFiles()
        {
            miArquivosUltimos.DropDownItems.Clear();
            if (Config.LastFiles.Count > 0)
                foreach (var f in Config.LastFiles)
                {
                    var mi = miArquivosUltimos.DropDownItems.Add(f);
                    mi.Click += (object sender, EventArgs e) =>
                    {
                        string arquivo = ((ToolStripItem)sender).Text;
                        if (!SetupData.Read(arquivo, out SetupData _setupData, out string message))
                        {
                            Dialog.Error(message);
                            return;
                        }
                        Read(_setupData);
                    };
                }
            miArquivosUltimos.Enabled = miArquivosUltimos.HasDropDownItems;
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
        private void MenuDistribuicaoClick(object sender, EventArgs e)
        {
            if (sender == miDistribuicaoGerar)
            {
                DistribuicaoGerar();
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
            chkJustOneInstance.Checked = setupData.JustOneInstance;
            txArguments.Text = setupData.Arguments ?? "";
            #endregion

            #region Atualizacao
            editUpdateSource.UpdateSource = setupData.UpdateSource;
            editUpdateSource.UpdateType = setupData.UpdateType;
            #endregion

            #region Extensões
            includeExt.Value = setupData.IncludeExtensions;
            ignoreExts.Value = setupData.IgnoredExtensions;
            #endregion

            setupData.Files.AddFilesFromFolder(setupFolder, setupData.IgnoredExtensions, setupData.IncludeExtensions);
            dgvFiles.RowCount = setupData.Files.Count;
            formSetupData = setupData;
            dgvFiles.AutoResizeColumns();
        }

        private void cmGrid_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dgvFiles.CurrentRow.Index < 0)
            {
                e.Cancel = true;
                return;
            }
            int index = dgvFiles.CurrentRow.Index;

            cmGridDesmarcarPasta.Text = "Todos da pasta " + formSetupData.Files[index].Folder;
            cmGridDesmarcarPasta.Tag = formSetupData.Files[index].Folder;

            cmGridMarcarPasta.Text = "Todos da pasta " + formSetupData.Files[index].Folder;
            cmGridMarcarPasta.Tag = formSetupData.Files[index].Folder;

            cmGridMarcarExtensao.Text = "Todos arquivos com extensão " + Path.GetExtension(formSetupData.Files[index].File);
            cmGridMarcarExtensao.Tag = Path.GetExtension(formSetupData.Files[index].File);

            cmGridDesmarcarExtensao.Text = "Todos arquivos com extensão " + Path.GetExtension(formSetupData.Files[index].File);
            cmGridDesmarcarExtensao.Tag = Path.GetExtension(formSetupData.Files[index].File);

        }

        private void GridMenuClick(object sender, EventArgs e)
        {
            ToolStripMenuItem m = (ToolStripMenuItem)sender;
            if (sender == cmGridDesmarcarExtensao)
            {
                foreach (var f in formSetupData.Files)
                    if (Path.GetExtension(f.File).Equals(m.Tag.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        f.FileFlags.Remove(SetupFileFlags.Include);
            }
            else if (sender == cmGridDesmarcarPasta)
            {
                foreach (var f in formSetupData.Files)
                    if (f.Folder.Equals(m.Tag.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        f.FileFlags.Remove(SetupFileFlags.Include);
            }
            else if (sender == cmGridMarcarExtensao)
            {
                foreach (var f in formSetupData.Files)
                    if (Path.GetExtension(f.File).Equals(m.Tag.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        f.FileFlags.Add(SetupFileFlags.Include);
            }
            else if (sender == cmGridMarcarPasta)
            {
                foreach (var f in formSetupData.Files)
                    if (f.Folder.Equals(m.Tag.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        f.FileFlags.Add(SetupFileFlags.Include);
            }
            else return;
            cmGrid.Refresh();
        }

        private void dgvFiles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvFiles.CurrentCell = dgvFiles.Rows[e.RowIndex].Cells[0];
            dgvFiles.Rows[e.RowIndex].Selected = true;
        }
    }
}
