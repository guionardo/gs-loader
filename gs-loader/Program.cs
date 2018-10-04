using gs_loader.Arguments;
using gs_loader.Base;
using gs_loader.Forms;
using gs_loader.Run;
using gs_loader.Setup;
using System;
using System.Windows.Forms;

namespace gs_loader
{
    static class Program
    {
        static NotifyIcon nIcon = new NotifyIcon();
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Exceptions.Install();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            nIcon.Icon = Properties.Resources.icon;
            UpdateIcon(UpdateIconType.Text, "GS-Loader");
            UpdateIcon(UpdateIconType.Visible, true);

            if (!SetupData.Create(@"A:\TBYTE", out SetupData setup, out string msg))
            {
                return;
            }

            if (DoRun.Run(setup, @"A:\TBYTE", out string message, UpdateIcon))
            {

            }
            Application.Run(new RunningForm(setup, @"A:\TBYTE"));
            /*
            TreatArguments.Parse(args);
            if (TreatArguments.OperationForm != null)
                Application.Run(TreatArguments.OperationForm);
                */
        }

        public static void UpdateIcon(UpdateIconType type, object value)
        {
            switch (type)
            {
                case UpdateIconType.Text:
                    nIcon.Text = value.ToString();
                    break;
                case UpdateIconType.Visible:
                    if (value is bool)
                        nIcon.Visible = (bool)value;
                    break;
            }
        }
    }
}
