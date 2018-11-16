using System.Windows.Forms;

namespace gs_loader_common.Forms
{
    public partial class WaitForm : Form
    {
        static WaitForm waitForm = null;

        WaitForm(string message)
        {
            InitializeComponent();
        }

        public static void Mostrar(string message)
        {
            if (waitForm == null)
                waitForm = new WaitForm(message);

            waitForm.lblMsg.Text = message;

            if (!waitForm.Visible)
                waitForm.Show();

            waitForm.Refresh();
        }

        public static void Fechar()
        {
            if (waitForm != null)
            {
                try { waitForm.Close(); }
                catch { }
                waitForm = null;
            }
        }

        /// <summary>
        /// Configura o progressbar
        /// </summary>
        /// <param name="maxValue">Menor que 1, desabilita</param>
        public static void SetupProgress(int maxValue)
        {
            if (waitForm == null)
                waitForm = new WaitForm("");
            if (maxValue < 1)
            {
                waitForm.progressBar1.Visible = false;
                return;
            }
            waitForm.progressBar1.Value = waitForm.progressBar1.Minimum;
            waitForm.progressBar1.Maximum = maxValue;
        }

        /// <summary>
        /// Define o valor da barra de progresso e opcionalmente define o texto
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        public static void SetProgress(int value, string message = null)
        {
            if (waitForm == null)
                return;
            if (value < waitForm.progressBar1.Minimum)
                value = waitForm.progressBar1.Minimum;
            else if (value > waitForm.progressBar1.Maximum)
                value = waitForm.progressBar1.Maximum;
            waitForm.progressBar1.Value = value;
            if (!string.IsNullOrEmpty(message))
                waitForm.lblMsg.Text = message;
            waitForm.Refresh();
        }
    }
}
