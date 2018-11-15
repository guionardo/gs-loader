namespace gs_loader_setup
{
    partial class MainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            gs_loader_common.Update.UpdateSource updateSource2 = new gs_loader_common.Update.UpdateSource();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.miArquivoNovo = new System.Windows.Forms.ToolStripMenuItem();
            this.miArquivoAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.miArquivoSalvar = new System.Windows.Forms.ToolStripMenuItem();
            this.distribuiçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miDistribuicaoGerar = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbExecutable = new System.Windows.Forms.ComboBox();
            this.txArguments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbExecutable = new System.Windows.Forms.GroupBox();
            this.chkJustOneInstance = new System.Windows.Forms.CheckBox();
            this.lblOriginFolder = new System.Windows.Forms.Label();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.clnLocalFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnPasta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnMD5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnInclude = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clnExecutable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gbIgnExts = new System.Windows.Forms.GroupBox();
            this.gbIncExts = new System.Windows.Forms.GroupBox();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.editUpdateSource = new gs_loader_common.Components.EditUpdateSource();
            this.ignoreExts = new gs_loader_common.Components.ExtensionsEdit();
            this.includeExt = new gs_loader_common.Components.ExtensionsEdit();
            this.menuStrip1.SuspendLayout();
            this.gbExecutable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.gbIgnExts.SuspendLayout();
            this.gbIncExts.SuspendLayout();
            this.tlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miArquivo,
            this.distribuiçãoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miArquivo
            // 
            this.miArquivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miArquivoNovo,
            this.miArquivoAbrir,
            this.miArquivoSalvar});
            this.miArquivo.Name = "miArquivo";
            this.miArquivo.Size = new System.Drawing.Size(61, 20);
            this.miArquivo.Text = "Arquivo";
            // 
            // miArquivoNovo
            // 
            this.miArquivoNovo.Name = "miArquivoNovo";
            this.miArquivoNovo.Size = new System.Drawing.Size(105, 22);
            this.miArquivoNovo.Text = "Novo";
            this.miArquivoNovo.Click += new System.EventHandler(this.MenuArquivoClick);
            // 
            // miArquivoAbrir
            // 
            this.miArquivoAbrir.Name = "miArquivoAbrir";
            this.miArquivoAbrir.Size = new System.Drawing.Size(105, 22);
            this.miArquivoAbrir.Text = "Abrir";
            this.miArquivoAbrir.Click += new System.EventHandler(this.MenuArquivoClick);
            // 
            // miArquivoSalvar
            // 
            this.miArquivoSalvar.Enabled = false;
            this.miArquivoSalvar.Name = "miArquivoSalvar";
            this.miArquivoSalvar.Size = new System.Drawing.Size(105, 22);
            this.miArquivoSalvar.Text = "Salvar";
            this.miArquivoSalvar.Click += new System.EventHandler(this.MenuArquivoClick);
            // 
            // distribuiçãoToolStripMenuItem
            // 
            this.distribuiçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDistribuicaoGerar});
            this.distribuiçãoToolStripMenuItem.Name = "distribuiçãoToolStripMenuItem";
            this.distribuiçãoToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.distribuiçãoToolStripMenuItem.Text = "Distribuição";
            // 
            // miDistribuicaoGerar
            // 
            this.miDistribuicaoGerar.Name = "miDistribuicaoGerar";
            this.miDistribuicaoGerar.Size = new System.Drawing.Size(150, 22);
            this.miDistribuicaoGerar.Text = "Gerar arquivos";
            this.miDistribuicaoGerar.Click += new System.EventHandler(this.MenuDistribuicaoClick);
            // 
            // cmbExecutable
            // 
            this.cmbExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExecutable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExecutable.FormattingEnabled = true;
            this.cmbExecutable.Location = new System.Drawing.Point(9, 18);
            this.cmbExecutable.Name = "cmbExecutable";
            this.cmbExecutable.Size = new System.Drawing.Size(379, 21);
            this.cmbExecutable.TabIndex = 5;
            // 
            // txArguments
            // 
            this.txArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txArguments.Location = new System.Drawing.Point(9, 96);
            this.txArguments.Name = "txArguments";
            this.txArguments.Size = new System.Drawing.Size(379, 20);
            this.txArguments.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Argumentos";
            // 
            // gbExecutable
            // 
            this.gbExecutable.Controls.Add(this.cmbExecutable);
            this.gbExecutable.Controls.Add(this.txArguments);
            this.gbExecutable.Controls.Add(this.label1);
            this.gbExecutable.Controls.Add(this.chkJustOneInstance);
            this.gbExecutable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbExecutable.Location = new System.Drawing.Point(3, 229);
            this.gbExecutable.Name = "gbExecutable";
            this.gbExecutable.Size = new System.Drawing.Size(394, 194);
            this.gbExecutable.TabIndex = 6;
            this.gbExecutable.TabStop = false;
            this.gbExecutable.Text = "Executável";
            // 
            // chkJustOneInstance
            // 
            this.chkJustOneInstance.AutoSize = true;
            this.chkJustOneInstance.Location = new System.Drawing.Point(9, 45);
            this.chkJustOneInstance.Name = "chkJustOneInstance";
            this.chkJustOneInstance.Size = new System.Drawing.Size(171, 17);
            this.chkJustOneInstance.TabIndex = 4;
            this.chkJustOneInstance.Text = "Permitir somente uma instância";
            this.chkJustOneInstance.UseVisualStyleBackColor = true;
            // 
            // lblOriginFolder
            // 
            this.lblOriginFolder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOriginFolder.AutoSize = true;
            this.tlp.SetColumnSpan(this.lblOriginFolder, 2);
            this.lblOriginFolder.Location = new System.Drawing.Point(3, 3);
            this.lblOriginFolder.Name = "lblOriginFolder";
            this.lblOriginFolder.Size = new System.Drawing.Size(70, 13);
            this.lblOriginFolder.TabIndex = 2;
            this.lblOriginFolder.Text = "Pasta Origem";
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnLocalFile,
            this.clnPasta,
            this.clnState,
            this.clnSize,
            this.clnMD5,
            this.clnInclude,
            this.clnExecutable});
            this.tlp.SetColumnSpan(this.dgvFiles, 2);
            this.dgvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFiles.Location = new System.Drawing.Point(3, 23);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.Size = new System.Drawing.Size(794, 130);
            this.dgvFiles.TabIndex = 3;
            this.dgvFiles.VirtualMode = true;
            this.dgvFiles.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellValueChanged);
            this.dgvFiles.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DGB_CellValueNeeded);
            this.dgvFiles.CurrentCellDirtyStateChanged += new System.EventHandler(this.DGVFiles_CurrentCellDirtyStateChanged);
            // 
            // clnLocalFile
            // 
            this.clnLocalFile.HeaderText = "Local";
            this.clnLocalFile.Name = "clnLocalFile";
            this.clnLocalFile.ReadOnly = true;
            this.clnLocalFile.Width = 58;
            // 
            // clnPasta
            // 
            this.clnPasta.HeaderText = "Pasta";
            this.clnPasta.Name = "clnPasta";
            this.clnPasta.ReadOnly = true;
            this.clnPasta.Width = 59;
            // 
            // clnState
            // 
            this.clnState.HeaderText = "Estado";
            this.clnState.Name = "clnState";
            this.clnState.ReadOnly = true;
            this.clnState.Width = 65;
            // 
            // clnSize
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.clnSize.DefaultCellStyle = dataGridViewCellStyle2;
            this.clnSize.HeaderText = "Tamanho";
            this.clnSize.Name = "clnSize";
            this.clnSize.ReadOnly = true;
            this.clnSize.Width = 77;
            // 
            // clnMD5
            // 
            this.clnMD5.HeaderText = "MD-5";
            this.clnMD5.Name = "clnMD5";
            this.clnMD5.ReadOnly = true;
            this.clnMD5.Width = 58;
            // 
            // clnInclude
            // 
            this.clnInclude.HeaderText = "Incluir";
            this.clnInclude.Name = "clnInclude";
            this.clnInclude.Width = 41;
            // 
            // clnExecutable
            // 
            this.clnExecutable.HeaderText = "Executável";
            this.clnExecutable.Name = "clnExecutable";
            this.clnExecutable.Width = 66;
            // 
            // gbIgnExts
            // 
            this.gbIgnExts.Controls.Add(this.ignoreExts);
            this.gbIgnExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbIgnExts.Location = new System.Drawing.Point(3, 159);
            this.gbIgnExts.Name = "gbIgnExts";
            this.gbIgnExts.Size = new System.Drawing.Size(394, 64);
            this.gbIgnExts.TabIndex = 1;
            this.gbIgnExts.TabStop = false;
            this.gbIgnExts.Text = "Extensões Ignoradas";
            // 
            // gbIncExts
            // 
            this.gbIncExts.Controls.Add(this.includeExt);
            this.gbIncExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbIncExts.Location = new System.Drawing.Point(403, 159);
            this.gbIncExts.Name = "gbIncExts";
            this.gbIncExts.Size = new System.Drawing.Size(394, 64);
            this.gbIncExts.TabIndex = 0;
            this.gbIncExts.TabStop = false;
            this.gbIncExts.Text = "Extensões Padrão";
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Controls.Add(this.lblOriginFolder, 0, 0);
            this.tlp.Controls.Add(this.editUpdateSource, 1, 3);
            this.tlp.Controls.Add(this.gbExecutable, 0, 3);
            this.tlp.Controls.Add(this.dgvFiles, 0, 1);
            this.tlp.Controls.Add(this.gbIgnExts, 0, 2);
            this.tlp.Controls.Add(this.gbIncExts, 1, 2);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(0, 24);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 4;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlp.Size = new System.Drawing.Size(800, 426);
            this.tlp.TabIndex = 8;
            // 
            // editUpdateSource
            // 
            this.editUpdateSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editUpdateSource.Location = new System.Drawing.Point(403, 229);
            this.editUpdateSource.Name = "editUpdateSource";
            this.editUpdateSource.Size = new System.Drawing.Size(394, 194);
            this.editUpdateSource.TabIndex = 5;
            updateSource2.Address = "";
            updateSource2.Password = "";
            updateSource2.Type = gs_loader_common.Update.UpdateSourceType.Folder;
            updateSource2.UserName = "";
            this.editUpdateSource.UpdateSource = updateSource2;
            this.editUpdateSource.UpdateType = gs_loader_common.Update.UpdateType.None;
            // 
            // ignoreExts
            // 
            this.ignoreExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ignoreExts.Location = new System.Drawing.Point(3, 16);
            this.ignoreExts.Name = "ignoreExts";
            this.ignoreExts.Padding = new System.Windows.Forms.Padding(2);
            this.ignoreExts.Size = new System.Drawing.Size(388, 45);
            this.ignoreExts.TabIndex = 0;
            this.ignoreExts.Value = new string[0];
            // 
            // includeExt
            // 
            this.includeExt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.includeExt.Location = new System.Drawing.Point(3, 16);
            this.includeExt.Name = "includeExt";
            this.includeExt.Padding = new System.Windows.Forms.Padding(2);
            this.includeExt.Size = new System.Drawing.Size(388, 45);
            this.includeExt.TabIndex = 0;
            this.includeExt.Value = new string[0];
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "GS-Loader Setup";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbExecutable.ResumeLayout(false);
            this.gbExecutable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.gbIgnExts.ResumeLayout(false);
            this.gbIncExts.ResumeLayout(false);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miArquivo;
        private System.Windows.Forms.ToolStripMenuItem miArquivoAbrir;
        private System.Windows.Forms.ToolStripMenuItem miArquivoSalvar;
        private System.Windows.Forms.ToolStripMenuItem miArquivoNovo;
        private gs_loader_common.Components.EditUpdateSource editUpdateSource;
        private System.Windows.Forms.ComboBox cmbExecutable;
        private System.Windows.Forms.TextBox txArguments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbExecutable;
        private System.Windows.Forms.CheckBox chkJustOneInstance;
        private System.Windows.Forms.Label lblOriginFolder;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.GroupBox gbIgnExts;
        private System.Windows.Forms.GroupBox gbIncExts;
        private System.Windows.Forms.ToolStripMenuItem distribuiçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miDistribuicaoGerar;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnLocalFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnPasta;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnState;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnMD5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnInclude;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnExecutable;
        private gs_loader_common.Components.ExtensionsEdit ignoreExts;
        private gs_loader_common.Components.ExtensionsEdit includeExt;
    }
}

