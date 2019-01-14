namespace gs_loader_common.Forms
{
    partial class EditSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSetup));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbVersion = new System.Windows.Forms.Label();
            this.txVersion = new System.Windows.Forms.TextBox();
            this.lbMainExecutable = new System.Windows.Forms.Label();
            this.txMainExecutable = new System.Windows.Forms.TextBox();
            this.lbArguments = new System.Windows.Forms.Label();
            this.txArguments = new System.Windows.Forms.TextBox();
            this.chkJustOneInstance = new System.Windows.Forms.CheckBox();
            this.lbRequirements = new System.Windows.Forms.Label();
            this.txRequirements = new System.Windows.Forms.TextBox();
            this.gbUpdateType = new System.Windows.Forms.GroupBox();
            this.chkUpdTypeBeforeRun = new System.Windows.Forms.CheckBox();
            this.chkUpdTypeAfterRun = new System.Windows.Forms.CheckBox();
            this.chkUpdTypeOnceADay = new System.Windows.Forms.CheckBox();
            this.lbRepositoryHost = new System.Windows.Forms.Label();
            this.txRepositoryHost = new System.Windows.Forms.TextBox();
            this.gbNotes = new System.Windows.Forms.GroupBox();
            this.txNotes = new System.Windows.Forms.TextBox();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.clnFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbUpdateType.SuspendLayout();
            this.gbNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // lbVersion
            // 
            resources.ApplyResources(this.lbVersion, "lbVersion");
            this.lbVersion.Name = "lbVersion";
            // 
            // txVersion
            // 
            resources.ApplyResources(this.txVersion, "txVersion");
            this.txVersion.Name = "txVersion";
            // 
            // lbMainExecutable
            // 
            resources.ApplyResources(this.lbMainExecutable, "lbMainExecutable");
            this.lbMainExecutable.Name = "lbMainExecutable";
            // 
            // txMainExecutable
            // 
            resources.ApplyResources(this.txMainExecutable, "txMainExecutable");
            this.txMainExecutable.Name = "txMainExecutable";
            this.txMainExecutable.ReadOnly = true;
            // 
            // lbArguments
            // 
            resources.ApplyResources(this.lbArguments, "lbArguments");
            this.lbArguments.Name = "lbArguments";
            // 
            // txArguments
            // 
            resources.ApplyResources(this.txArguments, "txArguments");
            this.txArguments.Name = "txArguments";
            // 
            // chkJustOneInstance
            // 
            resources.ApplyResources(this.chkJustOneInstance, "chkJustOneInstance");
            this.chkJustOneInstance.Name = "chkJustOneInstance";
            this.chkJustOneInstance.UseVisualStyleBackColor = true;
            // 
            // lbRequirements
            // 
            resources.ApplyResources(this.lbRequirements, "lbRequirements");
            this.lbRequirements.Name = "lbRequirements";
            // 
            // txRequirements
            // 
            resources.ApplyResources(this.txRequirements, "txRequirements");
            this.txRequirements.Name = "txRequirements";
            // 
            // gbUpdateType
            // 
            this.gbUpdateType.Controls.Add(this.chkUpdTypeOnceADay);
            this.gbUpdateType.Controls.Add(this.chkUpdTypeAfterRun);
            this.gbUpdateType.Controls.Add(this.chkUpdTypeBeforeRun);
            resources.ApplyResources(this.gbUpdateType, "gbUpdateType");
            this.gbUpdateType.Name = "gbUpdateType";
            this.gbUpdateType.TabStop = false;
            // 
            // chkUpdTypeBeforeRun
            // 
            resources.ApplyResources(this.chkUpdTypeBeforeRun, "chkUpdTypeBeforeRun");
            this.chkUpdTypeBeforeRun.Name = "chkUpdTypeBeforeRun";
            this.chkUpdTypeBeforeRun.UseVisualStyleBackColor = true;
            // 
            // chkUpdTypeAfterRun
            // 
            resources.ApplyResources(this.chkUpdTypeAfterRun, "chkUpdTypeAfterRun");
            this.chkUpdTypeAfterRun.Name = "chkUpdTypeAfterRun";
            this.chkUpdTypeAfterRun.UseVisualStyleBackColor = true;
            // 
            // chkUpdTypeOnceADay
            // 
            resources.ApplyResources(this.chkUpdTypeOnceADay, "chkUpdTypeOnceADay");
            this.chkUpdTypeOnceADay.Name = "chkUpdTypeOnceADay";
            this.chkUpdTypeOnceADay.UseVisualStyleBackColor = true;
            // 
            // lbRepositoryHost
            // 
            resources.ApplyResources(this.lbRepositoryHost, "lbRepositoryHost");
            this.lbRepositoryHost.Name = "lbRepositoryHost";
            // 
            // txRepositoryHost
            // 
            resources.ApplyResources(this.txRepositoryHost, "txRepositoryHost");
            this.txRepositoryHost.Name = "txRepositoryHost";
            // 
            // gbNotes
            // 
            this.gbNotes.Controls.Add(this.txNotes);
            resources.ApplyResources(this.gbNotes, "gbNotes");
            this.gbNotes.Name = "gbNotes";
            this.gbNotes.TabStop = false;
            // 
            // txNotes
            // 
            resources.ApplyResources(this.txNotes, "txNotes");
            this.txNotes.Name = "txNotes";
            // 
            // dgvFiles
            // 
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnFileName,
            this.clnVersion,
            this.clnSize});
            resources.ApplyResources(this.dgvFiles, "dgvFiles");
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.VirtualMode = true;
            this.dgvFiles.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvFiles_CellValueNeeded);
            // 
            // clnFileName
            // 
            resources.ApplyResources(this.clnFileName, "clnFileName");
            this.clnFileName.Name = "clnFileName";
            this.clnFileName.ReadOnly = true;
            // 
            // clnVersion
            // 
            resources.ApplyResources(this.clnVersion, "clnVersion");
            this.clnVersion.Name = "clnVersion";
            this.clnVersion.ReadOnly = true;
            // 
            // clnSize
            // 
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.clnSize.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.clnSize, "clnSize");
            this.clnSize.Name = "clnSize";
            this.clnSize.ReadOnly = true;
            // 
            // EditSetup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvFiles);
            this.Controls.Add(this.gbNotes);
            this.Controls.Add(this.txRepositoryHost);
            this.Controls.Add(this.lbRepositoryHost);
            this.Controls.Add(this.gbUpdateType);
            this.Controls.Add(this.txRequirements);
            this.Controls.Add(this.lbRequirements);
            this.Controls.Add(this.chkJustOneInstance);
            this.Controls.Add(this.txArguments);
            this.Controls.Add(this.lbArguments);
            this.Controls.Add(this.txMainExecutable);
            this.Controls.Add(this.lbMainExecutable);
            this.Controls.Add(this.txVersion);
            this.Controls.Add(this.lbVersion);
            this.Name = "EditSetup";
            this.gbUpdateType.ResumeLayout(false);
            this.gbUpdateType.PerformLayout();
            this.gbNotes.ResumeLayout(false);
            this.gbNotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.TextBox txVersion;
        private System.Windows.Forms.Label lbMainExecutable;
        private System.Windows.Forms.TextBox txMainExecutable;
        private System.Windows.Forms.Label lbArguments;
        private System.Windows.Forms.TextBox txArguments;
        private System.Windows.Forms.CheckBox chkJustOneInstance;
        private System.Windows.Forms.Label lbRequirements;
        private System.Windows.Forms.TextBox txRequirements;
        private System.Windows.Forms.GroupBox gbUpdateType;
        private System.Windows.Forms.CheckBox chkUpdTypeBeforeRun;
        private System.Windows.Forms.CheckBox chkUpdTypeOnceADay;
        private System.Windows.Forms.CheckBox chkUpdTypeAfterRun;
        private System.Windows.Forms.Label lbRepositoryHost;
        private System.Windows.Forms.TextBox txRepositoryHost;
        private System.Windows.Forms.GroupBox gbNotes;
        private System.Windows.Forms.TextBox txNotes;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnSize;
    }
}