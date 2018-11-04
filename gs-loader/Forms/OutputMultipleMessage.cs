using System.Windows.Forms;

namespace gs_loader.Forms
{
    public partial class OutputMultipleMessage : Form
    {
        public OutputMultipleMessage(string message, bool error)
        {
            InitializeComponent();
            textBox1.Text = message;
            Text = error ? "* ERRO *" : "GSLoader";
        }

        public static void Mostrar(string message, bool error)
        {
            Arguments.TreatArguments.OperationForm = new OutputMultipleMessage(message, error);
        }
    }
}
