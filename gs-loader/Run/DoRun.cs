using gs_loader.Base;
using gs_loader.Forms;
using gs_loader.Setup;
using gs_loader.Stats;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace gs_loader.Run
{
    public static class DoRun
    {
        const int SW_RESTORE = 9;
        const string USER32 = "USER32.DLL";

        static Process currentProcess = null;
        static BackgroundWorker ProcessWorker = new BackgroundWorker();
        public static bool IsRunning { get; private set; } = false;

        public static bool Run(SetupData setupData, string folder, out string message)
        {

            currentProcess = null;
            Directory.SetCurrentDirectory(folder);
            if (!File.Exists(setupData.Executable.File))
            {
                Log.Add(setupData.Executable.File + " INEXISTENTE", "ERRO");
                message = "Executável inexistente em " + setupData.Executable.File;
                return false;
            }

            if (setupData.JustOneInstance && InstancesRunning(setupData.Executable.File))
            {
                Log.Add(setupData.Executable.File + " JÁ EM EXECUÇÃO", "ERRO");
                message = "Instância já em execução";
                return false;
            }


            ProcessWorker = new BackgroundWorker();
            bool success = false;
            IsRunning = true;
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(Path.Combine(folder, setupData.Executable.File));
                NotifyLoader.UpdateIcon(UpdateIconType.SetIcon, icon);
            }
            catch (Exception e)
            {
                Log.Add(e.Message, "EXCEPTION");
            }

            ProcessWorker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                currentProcess = new Process();
                NotifyLoader.UpdateIcon(UpdateIconType.ProcessInfo, currentProcess);

                currentProcess.StartInfo.FileName = setupData.Executable.File;
                if (!string.IsNullOrEmpty(setupData.Arguments))
                    currentProcess.StartInfo.Arguments = setupData.Arguments;
                currentProcess.Exited += CurrentProcess_Exited;
                currentProcess.EnableRaisingEvents = true;

                try
                {
                    Log.Add(setupData.Executable.File, "START");
                    //DONE: Registrar log STARTING   
                    NotifyLoader.UpdateIcon(UpdateIconType.ShowBaloonInfo, setupData.Executable.Description);

                    currentProcess.Start();
                    currentProcess.WaitForExit();
                    DoStats.RegisterProcess(currentProcess);
                    NotifyLoader.UpdateIcon(UpdateIconType.ShowBaloonInfo, setupData.Executable.Description + " FINALIZANDO");
                    Log.Add(setupData.Executable.File + "\n" + currentProcess.StartTime + " -> " + currentProcess.ExitTime + "\n" +
                        "ExitCode: " + currentProcess.ExitCode + "\n" +
                        "TotalProcessorTime: " + currentProcess.TotalProcessorTime
                        , "STOP");
                    //DONE: Registrar log STARTED
                    success = true;
                }
                catch (Exception ex)
                {
                    Log.Add(ex.Message, "EXCEPTION");
                    //DONE: Registrar log START EXCEPTION

                }
                IsRunning = false;
                NotifyLoader.UpdateIcon(UpdateIconType.RestoreIcon, null);
            };
            ProcessWorker.RunWorkerAsync();
            message = "";
            return success;
        }

        private static void CurrentProcess_Exited(object sender, EventArgs e)
        {
            if (currentProcess == null)
            {
                //TODO: Registrar log de saída sem processo definido
            }
            else
            {
                //TODO: Registrar log de saída com data/hora de início, data/hora de saída e ExitCode                
            }
            NotifyLoader.UpdateIcon(UpdateIconType.ProcessInfo, (Process)null);
        }



        public static bool InstancesRunning(string file)
        {
            if (string.IsNullOrEmpty(file)) return false;
            try
            {
                var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(file));
                if (processes.Length == 0)
                    return false;

                NotifyLoader.UpdateIcon(UpdateIconType.ShowBalloonError, "Processo " + Path.GetFileNameWithoutExtension(file) + " já está em execução.");

                if (processes[0].MainWindowHandle != null)
                {
                    if (IsIconic(processes[0].MainWindowHandle))
                        ShowWindow(processes[0].MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(processes[0].MainWindowHandle);
                }
                return true;

            }
            catch (Exception e)
            {
                Log.Add("InstancesRunning(" + file + "):" + e.Message, "EXCEPTION");
                //DONE: Registrar exceção
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
