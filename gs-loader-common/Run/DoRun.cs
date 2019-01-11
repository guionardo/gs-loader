using gs_loader_common.Base;
using gs_loader_common.Forms;
using gs_loader_common.Programs;
using gs_loader_common.Stats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Run
{
    /// <summary>
    /// Classe para executar os comandos
    /// </summary>
    public static class DoRun
    {
        const int SW_RESTORE = 9;
        const string USER32 = "USER32.DLL";

        static Process currentProcess = null;
        static BackgroundWorker ProcessWorker = new BackgroundWorker();
        public static bool IsRunning { get; private set; } = false;

        [DllImport(USER32)]
        static extern bool IsIconic(IntPtr handle);
        [DllImport(USER32)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport(USER32)]
        static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        /// <summary>
        /// Executa uma programa a partir da sua instalação
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static bool Run(string basePath, out string message)
        {
            if (string.IsNullOrEmpty(basePath) ||
                !Directory.Exists(basePath))
            {
                message = "Parâmetro basePath vazio";
                return false;
            }
            Program program;
            bool success = false;
            message = "";
            try
            {
                program = Program.FromInstalledFolder(basePath);

                if (program.JustOneInstance && InstancesRunning(program.Main.FileName))
                {
                    Log.Add(program.Main.FileName + " JÁ EM EXECUÇÃO", "ERRO");
                    message = "Instância já em execução";
                    return false;
                }


                ProcessWorker = new BackgroundWorker();

                IsRunning = true;
                try
                {
                    Icon icon = Icon.ExtractAssociatedIcon(Path.Combine(basePath, program.Main.FileName));
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

                    currentProcess.StartInfo.FileName = program.Main.FileName;
                    currentProcess.StartInfo.WorkingDirectory = basePath;

                    if (!string.IsNullOrEmpty(program.Arguments))
                        currentProcess.StartInfo.Arguments = program.Arguments;
                    currentProcess.Exited += (object _sender, EventArgs _e) =>
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
                    };

                    currentProcess.EnableRaisingEvents = true;
                    SetupFolder setupFolder = new SetupFolder(basePath);
                    DoStats stats = new DoStats(setupFolder.DBPath);
                    try
                    {
                        Log.Add(program.Main.FileName, "START");
                        //DONE: Registrar log STARTING   
                        NotifyLoader.UpdateIcon(UpdateIconType.ShowBaloonInfo, program.ProgramName);
                        currentProcess.Start();
                        currentProcess.WaitForExit();
                        stats.RegisterProcess(currentProcess);
                        NotifyLoader.UpdateIcon(UpdateIconType.ShowBaloonInfo, program.ProgramName + " FINALIZANDO");
                        Log.Add(program.Main.FileName + "\n" + currentProcess.StartTime + " -> " + currentProcess.ExitTime + "\n" +
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
            }
            catch (Exception e)
            {
                message = "Exceção durante a execução: " + e.Message;
            }
            if (success)
                message = "Execução encerrou";

            return success;
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


    }
}
