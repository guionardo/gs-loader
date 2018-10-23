namespace gs_loader.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.gbIncExts = new System.Windows.Forms.GroupBox();
            this.gbIgnExts = new System.Windows.Forms.GroupBox();
            this.lblOriginFolder = new System.Windows.Forms.Label();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.clnLocalFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnPasta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnMD5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ignoreExts = new gs_loader.Components.DefaultExts();
            this.includeExts = new gs_loader.Components.DefaultExts();
            this.clnInclude = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gbIncExts.SuspendLayout();
            this.gbIgnExts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // gbIncExts
            // 
            this.gbIncExts.Controls.Add(this.includeExts);
            this.gbIncExts.Location = new System.Drawing.Point(273, 255);
            this.gbIncExts.Name = "gbIncExts";
            this.gbIncExts.Size = new System.Drawing.Size(200, 100);
            this.gbIncExts.TabIndex = 0;
            this.gbIncExts.TabStop = false;
            this.gbIncExts.Text = "Extensões Padrão";
            // 
            // gbIgnExts
            // 
            this.gbIgnExts.Controls.Add(this.ignoreExts);
            this.gbIgnExts.Location = new System.Drawing.Point(44, 271);
            this.gbIgnExts.Name = "gbIgnExts";
            this.gbIgnExts.Size = new System.Drawing.Size(200, 100);
            this.gbIgnExts.TabIndex = 1;
            this.gbIgnExts.TabStop = false;
            this.gbIgnExts.Text = "Extensões Ignoradas";
            // 
            // lblOriginFolder
            // 
            this.lblOriginFolder.AutoSize = true;
            this.lblOriginFolder.Location = new System.Drawing.Point(12, 9);
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
            this.dgvFiles.Location = new System.Drawing.Point(12, 25);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.Size = new System.Drawing.Size(619, 204);
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
            // ignoreExts
            // 
            this.ignoreExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ignoreExts.Location = new System.Drawing.Point(3, 16);
            this.ignoreExts.Name = "ignoreExts";
            this.ignoreExts.Padding = new System.Windows.Forms.Padding(2);
            this.ignoreExts.Size = new System.Drawing.Size(194, 81);
            this.ignoreExts.TabIndex = 0;
            this.ignoreExts.Value = new string[0];
            // 
            // includeExts
            // 
            this.includeExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.includeExts.Location = new System.Drawing.Point(3, 16);
            this.includeExts.Name = "includeExts";
            this.includeExts.Padding = new System.Windows.Forms.Padding(2);
            this.includeExts.Size = new System.Drawing.Size(194, 81);
            this.includeExts.TabIndex = 1;
            this.includeExts.Value = new string[0];
            // 
            // clnInclude
            // 
            this.clnInclude.HeaderText = "Incluir";
            this.clnInclude.Name = "clnInclude";
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvFiles);
            this.Controls.Add(this.lblOriginFolder);
            this.Controls.Add(this.gbIgnExts);
            this.Controls.Add(this.gbIncExts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupForm";
            this.Text = "SetupForm";
            this.gbIncExts.ResumeLayout(false);
            this.gbIgnExts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}