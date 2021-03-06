﻿using gs_loader.Arguments;
using gs_loader_common.Base;
using gs_loader_common.Forms;
using System;
using System.Threading;
using System.Windows.Forms;

namespace gs_loader
{
    static class Program
    {

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Exceptions.Install();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NotifyLoader.UpdateIcon(UpdateIconType.Text, "GS-Loader");
            NotifyLoader.UpdateIcon(UpdateIconType.Visible, true);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                //args = new string[] { "--help" };
                // RUN C:\TEMP
                //args = new string[] { "--run:C:\\TEMP" };
                args = new string[] { "--setup:A:\\TBYTE\\DISTRO", "--repositoryhost:B:\\REPOSITORIO" };
                //args = new string[] { "--verify:C:\\TEMP" };
                //args = new string[] { "--stats:C:\\TEMP"};
            }


            /*            SetupData setup = new SetupData
                        {
                            Executable = new SetupFile(@"A:\TBYTE\TBYTE.EXE", @"C:\WINDOWS"),
                            JustOneInstance = false
                        };
                        if (DoRun.Run(setup,@"A:\TBYTE",out string message))
                        {

                        }*/

            /*    if (!SetupData.Create(@"A:\TBYTE", out SetupData setup, out string msg))
                {
                    NotifyLoader.UpdateIcon(UpdateIconType.ShowBalloonError, msg);
                    return;
                }

                if (DoRun.Run(setup, @"A:\TBYTE", out string message))
                {

                }
                */


            TreatArguments.Parse(args);
            if (TreatArguments.OperationForm != null)
            {
                Application.Run(TreatArguments.OperationForm);
            }

            while (gs_loader_common.Run.DoRun.IsRunning)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            NotifyLoader.UpdateIcon(UpdateIconType.Visible, false);

            return Output.ExitCode;
        }


    }
}
