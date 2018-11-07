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
        }

        public static void Fechar()
        {
            if (waitForm != null)
            {
                try
                {
                    waitForm.Close();
                }
                catch { }
                waitForm = null;
            }
        }
    }
}
