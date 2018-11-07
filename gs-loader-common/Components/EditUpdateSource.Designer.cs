namespace gs_loader_common.Components
{
    partial class EditUpdateSource
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
            this.gbUpdateSource = new System.Windows.Forms.GroupBox();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txPass = new System.Windows.Forms.TextBox();
            this.cmbUpdateType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txUser = new System.Windows.Forms.TextBox();
            this.chkOnceADay = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbUpdateSource = new System.Windows.Forms.ComboBox();
            this.txAddress = new System.Windows.Forms.TextBox();
            this.lblAdress = new System.Windows.Forms.Label();
            this.gbUpdateSource.SuspendLayout();
            this.tlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUpdateSource
            // 
            this.gbUpdateSource.Controls.Add(this.tlp);
            this.gbUpdateSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUpdateSource.Location = new System.Drawing.Point(0, 0);
            this.gbUpdateSource.Name = "gbUpdateSource";
            this.gbUpdateSource.Size = new System.Drawing.Size(350, 289);
            this.gbUpdateSource.TabIndex = 0;
            this.gbUpdateSource.TabStop = false;
            this.gbUpdateSource.Text = "Configurações de Atualização";
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 2;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.51463F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.48537F));
            this.tlp.Controls.Add(this.label1, 0, 0);
            this.tlp.Controls.Add(this.txPass, 1, 5);
            this.tlp.Controls.Add(this.cmbUpdateType, 1, 0);
            this.tlp.Controls.Add(this.label4, 0, 5);
            this.tlp.Controls.Add(this.txUser, 1, 4);
            this.tlp.Controls.Add(this.chkOnceADay, 1, 1);
            this.tlp.Controls.Add(this.label2, 0, 2);
            this.tlp.Controls.Add(this.label3, 0, 4);
            this.tlp.Controls.Add(this.cmbUpdateSource, 1, 2);
            this.tlp.Controls.Add(this.txAddress, 1, 3);
            this.tlp.Controls.Add(this.lblAdress, 0, 3);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(3, 16);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 6;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.Size = new System.Drawing.Size(344, 270);
            this.tlp.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.tlp.SetRowSpan(this.label1, 2);
            this.label1.Size = new System.Drawing.Size(48, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quando atualizar";
            // 
            // txPass
            // 
            this.txPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txPass.Location = new System.Drawing.Point(77, 235);
            this.txPass.Name = "txPass";
            this.txPass.Size = new System.Drawing.Size(264, 20);
            this.txPass.TabIndex = 10;
            // 
            // cmbUpdateType
            // 
            this.cmbUpdateType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUpdateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUpdateType.FormattingEnabled = true;
            this.cmbUpdateType.Items.AddRange(new object[] {
            "Nunca",
            "Antes de executar",
            "Após executar"});
            this.cmbUpdateType.Location = new System.Drawing.Point(77, 11);
            this.cmbUpdateType.Name = "cmbUpdateType";
            this.cmbUpdateType.Size = new System.Drawing.Size(264, 21);
            this.cmbUpdateType.TabIndex = 1;
            this.cmbUpdateType.SelectedIndexChanged += new System.EventHandler(this.cmbUpdateType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Senha";
            // 
            // txUser
            // 
            this.txUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txUser.Location = new System.Drawing.Point(77, 188);
            this.txUser.Name = "txUser";
            this.txUser.Size = new System.Drawing.Size(264, 20);
            this.txUser.TabIndex = 9;
            // 
            // chkOnceADay
            // 
            this.chkOnceADay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkOnceADay.AutoSize = true;
            this.chkOnceADay.Location = new System.Drawing.Point(77, 57);
            this.chkOnceADay.Name = "chkOnceADay";
            this.chkOnceADay.Size = new System.Drawing.Size(100, 17);
            this.chkOnceADay.TabIndex = 2;
            this.chkOnceADay.Text = "Uma vez ao dia";
            this.chkOnceADay.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Origem";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Usuário";
            // 
            // cmbUpdateSource
            // 
            this.cmbUpdateSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUpdateSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUpdateSource.FormattingEnabled = true;
            this.cmbUpdateSource.Items.AddRange(new object[] {
            "Pasta",
            "FTP",
            "HTTP"});
            this.cmbUpdateSource.Location = new System.Drawing.Point(77, 99);
            this.cmbUpdateSource.Name = "cmbUpdateSource";
            this.cmbUpdateSource.Size = new System.Drawing.Size(264, 21);
            this.cmbUpdateSource.TabIndex = 4;
            this.cmbUpdateSource.SelectedIndexChanged += new System.EventHandler(this.cmbUpdateSource_SelectedIndexChanged);
            // 
            // txAddress
            // 
            this.txAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txAddress.Location = new System.Drawing.Point(77, 144);
            this.txAddress.Name = "txAddress";
            this.txAddress.Size = new System.Drawing.Size(264, 20);
            this.txAddress.TabIndex = 6;
            // 
            // lblAdress
            // 
            this.lblAdress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAdress.AutoSize = true;
            this.lblAdress.Location = new System.Drawing.Point(3, 141);
            this.lblAdress.Name = "lblAdress";
            this.lblAdress.Size = new System.Drawing.Size(56, 26);
            this.lblAdress.TabIndex = 5;
            this.lblAdress.Text = "Endereço Origem";
            // 
            // EditUpdateSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbUpdateSource);
            this.Name = "EditUpdateSource";
            this.Size = new System.Drawing.Size(350, 289);
            this.gbUpdateSource.ResumeLayout(false);
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUpdateSource;
        private System.Windows.Forms.ComboBox cmbUpdateType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkOnceADay;
        private System.Windows.Forms.ComboBox cmbUpdateSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAdress;
        private System.Windows.Forms.TextBox txAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txPass;
        private System.Windows.Forms.TextBox txUser;
        private System.Windows.Forms.TableLayoutPanel tlp;
    }
}
