namespace gs_loader_common.Components
{
    partial class DefaultExts
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

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txExts = new System.Windows.Forms.TextBox();
            this.cmOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miReset = new System.Windows.Forms.ToolStripMenuItem();
            this.miDefaultValues = new System.Windows.Forms.ToolStripMenuItem();
            this.miValidate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txExts
            // 
            this.txExts.ContextMenuStrip = this.cmOptions;
            this.txExts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txExts.Location = new System.Drawing.Point(2, 2);
            this.txExts.Multiline = true;
            this.txExts.Name = "txExts";
            this.txExts.Size = new System.Drawing.Size(146, 146);
            this.txExts.TabIndex = 0;
            this.txExts.Validating += new System.ComponentModel.CancelEventHandler(this.txExts_Validating);
            // 
            // cmOptions
            // 
            this.cmOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miReset,
            this.miDefaultValues,
            this.miValidate});
            this.cmOptions.Name = "cmOptions";
            this.cmOptions.Size = new System.Drawing.Size(174, 70);
            // 
            // miReset
            // 
            this.miReset.Name = "miReset";
            this.miReset.Size = new System.Drawing.Size(173, 22);
            this.miReset.Text = "Reset";
            this.miReset.Click += new System.EventHandler(this.ContextMenuClick);
            // 
            // miDefaultValues
            // 
            this.miDefaultValues.Name = "miDefaultValues";
            this.miDefaultValues.Size = new System.Drawing.Size(173, 22);
            this.miDefaultValues.Text = "Default values";
            this.miDefaultValues.Click += new System.EventHandler(this.ContextMenuClick);
            // 
            // miValidate
            // 
            this.miValidate.Name = "miValidate";
            this.miValidate.Size = new System.Drawing.Size(173, 22);
            this.miValidate.Text = "Validate extensions";
            this.miValidate.Click += new System.EventHandler(this.ContextMenuClick);
            // 
            // DefaultExts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txExts);
            this.Name = "DefaultExts";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.cmOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txExts;
        private System.Windows.Forms.ContextMenuStrip cmOptions;
        private System.Windows.Forms.ToolStripMenuItem miReset;
        private System.Windows.Forms.ToolStripMenuItem miDefaultValues;
        private System.Windows.Forms.ToolStripMenuItem miValidate;
    }
}
