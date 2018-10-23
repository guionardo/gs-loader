using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gs_loader.Components
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
            get => GetUnique(txExts.Lines);
            set
            {
                if (value == null)
                    return;
                if (InitialValue == null || InitialValue.Length == 0)
                    InitialValue = CopyArray(value);

                txExts.Lines = GetUnique(value);
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
                txExts.Lines = GetUnique(txExts.Lines);
            }
        }

        /// <summary>
        /// Assegura que não existam itens duplicados
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        string[] GetUnique(string[] values)
        {
            List<string> listaV = new List<string>();
            foreach (var lin in values)
            {
                string l = lin;
                // Remove * e . do início da extensão
                while (l.StartsWith(".") || l.StartsWith("*"))
                    l = l.Substring(1);

                l = "." + l;

                bool found = false;
                foreach (var i in listaV)
                    if (l.Equals(i, StringComparison.InvariantCultureIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                if (!found)
                    listaV.Add(l);
            }
            return listaV.ToArray();
        }

        private void txExts_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txExts.Lines = GetUnique(txExts.Lines);
        }
    }
}
