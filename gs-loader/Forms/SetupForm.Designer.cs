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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.gbIncExts = new System.Windows.Forms.GroupBox();
            this.includeExts = new gs_loader.Components.DefaultExts();
            this.gbIgnExts = new System.Windows.Forms.GroupBox();
            this.ignoreExts = new gs_loader.Components.DefaultExts();
            this.gbIncExts.SuspendLayout();
            this.gbIgnExts.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbIncExts
            // 
            this.gbIncExts.Controls.Add(this.includeExts);
            this.gbIncExts.Location = new System.Drawing.Point(280, 180);
            this.gbIncExts.Name = "gbIncExts";
            this.gbIncExts.Size = new System.Drawing.Size(200, 100);
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
            this.includeExts.Size = new System.Drawing.Size(194, 81);
            this.includeExts.TabIndex = 1;
            this.includeExts.Value = new string[0];
            // 
            // gbIgnExts
            // 
            this.gbIgnExts.Controls.Add(this.ignoreExts);
            this.gbIgnExts.Location = new System.Drawing.Point(43, 209);
            this.gbIgnExts.Name = "gbIgnExts";
            this.gbIgnExts.Size = new System.Drawing.Size(200, 100);
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
            this.ignoreExts.Size = new System.Drawing.Size(194, 81);
            this.ignoreExts.TabIndex = 0;
            this.ignoreExts.Value = new string[0];
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbIgnExts);
            this.Controls.Add(this.gbIncExts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetupForm";
            this.Text = "SetupForm";
            this.gbIncExts.ResumeLayout(false);
            this.gbIgnExts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbIncExts;
        private Components.DefaultExts includeExts;
        private System.Windows.Forms.GroupBox gbIgnExts;
        private Components.DefaultExts ignoreExts;
    }
}