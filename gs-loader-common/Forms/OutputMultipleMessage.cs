using System.Windows.Forms;

namespace gs_loader_common.Forms
{
    public partial class OutputMultipleMessage : Form
    {
        public static Form CurrentForm { get; private set; } = null;
        public OutputMultipleMessage(string message, bool error)
        {
            InitializeComponent();
            textBox1.Text = message;
            Text = error ? "* ERRO *" : "GSLoader";
        }

        public static void Mostrar(string message, bool error)
        {
            CurrentForm = new OutputMultipleMessage(message, error);           
        }
    }
}
