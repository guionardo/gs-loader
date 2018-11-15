using gs_loader_common.Base;
using System;
using System.Windows.Forms;

namespace gs_loader_common.Components
{
    public partial class DefaultExts : UserControl
    {
        /// <summary>
        /// Valores padrão
        /// </summary>
        public string[] DefaultValues = null;

        /// <summary>
        /// Valor inicial utilizado na função de reset
        /// </summary>
        private string[] InitialValue = null;

        public DefaultExts()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Extensões
        /// </summary>
        public string[] Value
        {
            get => txExts.Lines.GetUnique();
            set
            {
                if (value == null)
                    return;
                if (InitialValue == null || InitialValue.Length == 0)
                    InitialValue = (string[])value.Clone();

                txExts.Lines = value.GetUnique();
            }
        }
        private static string[] CopyArray(string[] origin)
        {

            if (origin == null || origin.Length == 0)
                return new string[0];


            string[] destiny = new string[origin.Length];
            for (int i = 0; i < origin.Length; i++)
                destiny[i] = origin[i];
            return destiny;
        }

        private void ContextMenuClick(object sender, EventArgs e)
        {
            if (sender == miReset)
            {
                Value = CopyArray(InitialValue);
            }
            else if (sender == miDefaultValues)
            {
                if (DefaultValues == null)
                    return;

                Value = CopyArray(DefaultValues);
            }
            else if (sender == miValidate)
            {
                txExts.Lines = txExts.Lines.GetUnique();
            }
        }



        private void txExts_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txExts.Lines = txExts.Lines.GetUnique();
        }
    }
}
