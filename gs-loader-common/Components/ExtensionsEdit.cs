using System;
using System.Windows.Forms;
using gs_loader_common.Base;

namespace gs_loader_common.Components
{
    public partial class ExtensionsEdit : UserControl
    {
        /// <summary>
        /// Valores padrão
        /// </summary>
        public string[] DefaultValues = null;

        /// <summary>
        /// Valor inicial utilizado na função de reset
        /// </summary>
        private string[] InitialValue = null;
        private string[] _value = new string[0];

        public ExtensionsEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Extensões
        /// </summary>
        public string[] Value
        {
            get => _value.GetUnique();
            set
            {
                if (value == null)
                    return;
                if (InitialValue == null || InitialValue.Length == 0)
                    InitialValue = (string[])value.Clone();

                _value = value.GetUnique();
                btnExt.Text = "";
                if (_value.Length == 0)
                {
                    btnExt.Text = "Sem valores";
                    return;
                }
                string tooltips = "";
                for (int i = 0; i < _value.Length; i++)
                {
                    btnExt.Text += _value[i] + (i < _value.Length - 1 ? " " : "");
                    tooltips += _value[i] + " " + Extensions.MimeType(_value[i]) + "\n";
                }
                toolTip1.SetToolTip(btnExt, tooltips);
            }
        }

        private void ContextMenuClick(object sender, EventArgs e)
        {
            if (sender == miReset)
            {
                Value = InitialValue.GetUnique();
            }
            else if (sender == miDefaultValues)
            {
                if (DefaultValues == null)
                    return;

                Value = DefaultValues.GetUnique();
            }
            else if (sender == miValidate)
            {
                Value = Value.GetUnique();
            }
        }

        private void btnExt_Click(object sender, EventArgs e)
        {
            string exts = "";
            foreach (var ex in _value)
                exts += ex + " ";
            exts = Microsoft.VisualBasic.Interaction.InputBox("Extensões (separadas por espaço)", "Extensões", exts.Trim());
            if (string.IsNullOrEmpty(exts))
            {
                if (Dialog.YesNo("Deseja informar uma lista vazia de extensões?"))
                    Value = new string[0];
                return;
            }
            var _v = exts.ToLowerInvariant().Split(' ');
            for (int i = 0; i < _v.Length; i++)
                if (!_v[i].StartsWith("."))
                    _v[i] = "." + _v[i];
            Value = _v.GetUnique();
        }
    }
}

