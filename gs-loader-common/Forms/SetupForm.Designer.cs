namespace gs_loader_common.Forms
{
    partial class SetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            gs_loader_common.Update.UpdateSource updateSource1 = new gs_loader_common.Update.UpdateSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.gbIncExts = new System.Windows.Forms.GroupBox();
            this.includeExts = new gs_loader_common.Components.DefaultExts();
            this.gbIgnExts = new System.Windows.Forms.GroupBox();
            this.ignoreExts = new gs_loader_common.Components.DefaultExts();
            this.lblOriginFolder = new System.Windows.Forms.Label();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.clnLocalFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnPasta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnMD5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnInclude = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkJustOneInstance = new System.Windows.Forms.CheckBox();
            this.gbExecutable = new System.Windows.Forms.GroupBox();
            this.txArguments = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txExecutable = new System.Windows.Forms.TextBox();
            this.editUpdateSource = new gs_loader_common.Components.EditUpdateSource();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.gbIncExts.SuspendLayout();
            this.gbIgnExts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.gbExecutable.SuspendLayout();
            this.tlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbIncExts
            // 
            this.gbIncExts.Controls.Add(this.includeExts);
            this.gbIncExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbIncExts.Location = new System.Drawing.Point(403, 123);
            this.gbIncExts.Name = "gbIncExts";
            this.gbIncExts.Size = new System.Drawing.Size(394, 94);
            this.gbIncExts.TabIndex = 0;
            this.gbIncExts.TabStop = false;
            this.gbIncExts.Text = "Extensões Padrão";
            // 
            // includeExts
            // 
            this.includeExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.includeExts.Location = new System.Drawing.Point(3, 16);
            this.includeExts.Name = "includeExts";
            this.includeExts.Padding = new System.Windows.Forms.Padding(2);
            this.includeExts.Size = new System.Drawing.Size(388, 75);
            this.includeExts.TabIndex = 1;
            this.includeExts.Value = new string[0];
            // 
            // gbIgnExts
            // 
            this.gbIgnExts.Controls.Add(this.ignoreExts);
            this.gbIgnExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbIgnExts.Location = new System.Drawing.Point(3, 123);
            this.gbIgnExts.Name = "gbIgnExts";
            this.gbIgnExts.Size = new System.Drawing.Size(394, 94);
            this.gbIgnExts.TabIndex = 1;
            this.gbIgnExts.TabStop = false;
            this.gbIgnExts.Text = "Extensões Ignoradas";
            // 
            // ignoreExts
            // 
            this.ignoreExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ignoreExts.Location = new System.Drawing.Point(3, 16);
            this.ignoreExts.Name = "ignoreExts";
            this.ignoreExts.Padding = new System.Windows.Forms.Padding(2);
            this.ignoreExts.Size = new System.Drawing.Size(388, 75);
            this.ignoreExts.TabIndex = 0;
            this.ignoreExts.Value = new string[0];
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
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnLocalFile,
            this.clnPasta,
            this.clnState,
            this.clnSize,
            this.clnMD5,
            this.clnInclude});
            this.tlp.SetColumnSpan(this.dgvFiles, 2);
            this.dgvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFiles.Location = new System.Drawing.Point(3, 23);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.Size = new System.Drawing.Size(794, 94);
            this.dgvFiles.TabIndex = 3;
            this.dgvFiles.VirtualMode = true;
            this.dgvFiles.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFiles_CellValueChanged);
            this.dgvFiles.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvFiles_CellValueNeeded);
            // 
            // clnLocalFile
            // 
            this.clnLocalFile.HeaderText = "Local";
            this.clnLocalFile.Name = "clnLocalFile";
            this.clnLocalFile.ReadOnly = true;
            // 
            // clnPasta
            // 
            this.clnPasta.HeaderText = "Pasta";
            this.clnPasta.Name = "clnPasta";
            this.clnPasta.ReadOnly = true;
            // 
            // clnState
            // 
            this.clnState.HeaderText = "Estado";
            this.clnState.Name = "clnState";
            this.clnState.ReadOnly = true;
            // 
            // clnSize
            // 
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.clnSize.DefaultCellStyle = dataGridViewCellStyle1;
            this.clnSize.HeaderText = "Tamanho";
            this.clnSize.Name = "clnSize";
            this.clnSize.ReadOnly = true;
            // 
            // clnMD5
            // 
            this.clnMD5.HeaderText = "MD-5";
            this.clnMD5.Name = "clnMD5";
            this.clnMD5.ReadOnly = true;
            // 
            // clnInclude
            // 
            this.clnInclude.HeaderText = "Incluir";
            this.clnInclude.Name = "clnInclude";
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
            // gbExecutable
            // 
            this.gbExecutable.Controls.Add(this.comboBox1);
            this.gbExecutable.Controls.Add(this.txArguments);
            this.gbExecutable.Controls.Add(this.label1);
            this.gbExecutable.Controls.Add(this.chkJustOneInstance);
            this.gbExecutable.Controls.Add(this.txExecutable);
            this.gbExecutable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbExecutable.Location = new System.Drawing.Point(3, 223);
            this.gbExecutable.Name = "gbExecutable";
            this.gbExecutable.Size = new System.Drawing.Size(394, 194);
            this.gbExecutable.TabIndex = 6;
            this.gbExecutable.TabStop = false;
            this.gbExecutable.Text = "Executável";
            // 
            // txArguments
            // 
            this.txArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txArguments.Location = new System.Drawing.Point(9, 96);
            this.txArguments.Name = "txArguments";
            this.txArguments.Size = new System.Drawing.Size(382, 20);
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
            // txExecutable
            // 
            this.txExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txExecutable.Location = new System.Drawing.Point(10, 19);
            this.txExecutable.Name = "txExecutable";
            this.txExecutable.Size = new System.Drawing.Size(382, 20);
            this.txExecutable.TabIndex = 0;
            // 
            // editUpdateSource
            // 
            this.editUpdateSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editUpdateSource.Location = new System.Drawing.Point(403, 223);
            this.editUpdateSource.Name = "editUpdateSource";
            this.editUpdateSource.Size = new System.Drawing.Size(394, 194);
            this.editUpdateSource.TabIndex = 5;
            updateSource1.Address = "";
            updateSource1.Password = "";
            updateSource1.Type = gs_loader_common.Update.UpdateSourceType.Folder;
            updateSource1.UserName = "";
            this.editUpdateSource.UpdateSource = updateSource1;
            this.editUpdateSource.UpdateType = gs_loader_common.Update.UpdateType.None;
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
            this.tlp.Controls.Add(this.btnOK, 0, 4);
            this.tlp.Controls.Add(this.btnCancelar, 1, 4);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 5;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp.Size = new System.Drawing.Size(800, 450);
            this.tlp.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.Location = new System.Drawing.Point(162, 423);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancelar.Location = new System.Drawing.Point(562, 423);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.ButtonClick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(10, 135);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupForm";
            this.Text = "SetupForm";
            this.gbIncExts.ResumeLayout(false);
            this.gbIgnExts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.gbExecutable.ResumeLayout(false);
            this.gbExecutable.PerformLayout();
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbIncExts;
        private Components.DefaultExts includeExts;
        private System.Windows.Forms.GroupBox gbIgnExts;
        private Components.DefaultExts ignoreExts;
        private System.Windows.Forms.Label lblOriginFolder;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnLocalFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnPasta;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnState;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnMD5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnInclude;
        private System.Windows.Forms.CheckBox chkJustOneInstance;
        private Components.EditUpdateSource editUpdateSource;
        private System.Windows.Forms.GroupBox gbExecutable;
        private System.Windows.Forms.TextBox txExecutable;
        private System.Windows.Forms.TextBox txArguments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}