using System.Windows.Forms;

namespace gs_loader.Base
{
    public static class Dialog
    {
        public static void Message(string message, string caption = null)
        {
            MessageBox.Show(message, caption??"GS-Loader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Error(string message, string caption = "ERROR")
        {
            MessageBox.Show(message, caption ?? "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

    }
}
