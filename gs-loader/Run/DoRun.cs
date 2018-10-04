using gs_loader.Forms;
using gs_loader.Setup;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace gs_loader.Run
{
    public static class DoRun
    {
        const int SW_RESTORE = 9;

        const string USER32 = "USER32.DLL";
        static Process currentProcess = null;

        public static bool Run(SetupData setupData, string folder, out string message, Action<UpdateIconType,object> updateNotify)
        {
            currentProcess = null;
            Directory.SetCurrentDirectory(folder);
            if (!File.Exists(setupData.Executable.File))
            {
                message = "Executável inexistente em " + setupData.Executable.File;
                return false;
            }

            if (setupData.JustOneInstance && InstancesRunning(setupData.Executable.File))
            {
                message = "Instância já em execução";
                return false;
            }
            currentProcess = new Process();
            currentProcess.StartInfo.FileName = setupData.Executable.File;
            if (!string.IsNullOrEmpty(setupData.Arguments))
                currentProcess.StartInfo.Arguments = setupData.Arguments;
            currentProcess.Exited += (object sender, EventArgs e) =>
        {
            if (currentProcess == null)
            {
                //TODO: Registrar log de saída sem processo definido
            }
            else
            {
                //TODO: Registrar log de saída com data/hora de início, data/hora de saída e ExitCode                
            }
        };
            currentProcess.EnableRaisingEvents = true;
            try
            {
                //TODO: Registrar log STARTING                
                currentProcess.Start();
                currentProcess.WaitForExit();
                //TODO: Registrar log STARTED
                message = "OK";
                return true;
            }
            catch (Exception e)
            {
                //TODO: Registrar log START EXCEPTION
                message = e.Message;

            }
            return false;
        }

        private static bool InstancesRunning(string file)
        {
            try
            {
                var processes = Process.GetProcessesByName(Path.GetFileName(file));
                if (processes.Length == 0)
                    return false;
                foreach (var p in processes)
                    if (p.StartInfo.FileName.Equals(file, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (p.MainWindowHandle != null)
                        {
                            if (IsIconic(p.MainWindowHandle))
                                ShowWindow(p.MainWindowHandle, SW_RESTORE);
                            SetForegroundWindow(p.MainWindowHandle);
                        }
                        return true;
                    }
            }
            catch (Exception e)
            {
                //TODO: Registrar exceção
            }

            return false;
        }

        [DllImport(USER32)]
        static extern bool IsIconic(IntPtr handle);

        [DllImport(USER32)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport(USER32)]
        static extern bool ShowWindow(IntPtr handle, int nCmdShow);
    }


}
