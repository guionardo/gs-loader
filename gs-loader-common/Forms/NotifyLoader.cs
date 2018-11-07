using gs_loader_common.Base;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace gs_loader_common.Forms
{
    public class NotifyLoader
    {
        static ContextMenuStrip cmIcon;
        static NotifyIcon nIcon;
        static NotifyLoader()
        {
            cmIcon = new ContextMenuStrip();
            AddMenuItem("Informações do processo", MenuItemType.ShowInfo);
            AddMenuItem("Estatísticas do processo", MenuItemType.Stats);
            AddMenuItem("Fechar processo", MenuItemType.Close);
            AddMenuItem("Fechar processo (forçar)", MenuItemType.ForceClose);

            nIcon = new NotifyIcon
            {
                Icon = Properties.Resources.icon,
                Text = "GS-Loader",
                ContextMenuStrip = cmIcon
            };

        }

        public static string ProcessDescription { get; private set; } = "GSLoader";
        public static Process ProcessInfo { get; private set; } = null;
        public static void UpdateIcon(UpdateIconType type, object value)
        {
            if (!nIcon.Visible)
                nIcon.Visible = true;
            switch (type)
            {
                case UpdateIconType.ProcessDescription:
                    if (value is string)
                        ProcessDescription = value.ToString();
                    break;

                case UpdateIconType.Text:
                    nIcon.Text = value.ToString();
                    break;

                case UpdateIconType.Visible:
                    if (value is bool)
                        nIcon.Visible = (bool)value;
                    break;

                case UpdateIconType.ProcessInfo:
                    if (value is Process)
                        ProcessInfo = (Process)value;
                    break;

                case UpdateIconType.ShowBaloonInfo:
                    if (value is string)
                        nIcon.ShowBalloonTip(10000, ProcessDescription, value.ToString(), ToolTipIcon.Info);

                    break;

                case UpdateIconType.ShowBalloonError:
                    if (value is string)
                        nIcon.ShowBalloonTip(10000, ProcessDescription, value.ToString(), ToolTipIcon.Error);
                    break;

                case UpdateIconType.SetIcon:
                    if (value is Icon)
                        nIcon.Icon = (Icon)value;
                    break;

                case UpdateIconType.RestoreIcon:
                    nIcon.Icon = Properties.Resources.icon;
                    break;
            }
        }

        static void AddMenuItem(string text, MenuItemType type)
        {
            var menuitem = cmIcon.Items.Add(text);
            menuitem.Tag = type;
            menuitem.Click += MenuItemClick;
        }

        private static void CloseProcess(bool force)
        {
            if (force)
            {
                try
                {
                    ProcessInfo.Kill();
                    Log.Add(ProcessInfo.ProcessName, "KILL");
                }
                catch (Exception e)
                {
                    Log.Add("KILL: " + e.Message, "EXCEPTION");
                }
            }
            else
            {
                try
                {
                    if (ProcessInfo.CloseMainWindow())
                    {
                        Log.Add(ProcessInfo.ProcessName, "CLOSED");
                    }
                    else if (Dialog.YesNo("O processo não respondeu ao comando de fechamento.\n" +
                      "Deseja tentar novamente, agora forçando o fechamento?"))
                        CloseProcess(true);
                }
                catch (Exception e)
                {
                    Log.Add("CLOSE: " + e.Message, "EXCEPTION");
                }
            }

        }

        private static void MenuItemClick(object sender, EventArgs e)
        {
            if (ProcessInfo == null)
            {
                UpdateIcon(UpdateIconType.ShowBalloonError, "Não há processo em execução monitorada!");
                return;
            }
            switch ((MenuItemType)((ToolStripItem)sender).Tag)
            {
                case MenuItemType.ShowInfo:
                    ShowProcessInfo();

                    break;
                case MenuItemType.Stats:
                    //TODO: Mostrar estatísticas do processo
                    break;

                case MenuItemType.Close:
                    CloseProcess(false);
                    break;
                case MenuItemType.ForceClose:
                    CloseProcess(true);
                    break;
            }
        }
        /// <summary>
        /// Mostrar informações do processo em execução
        /// </summary>
        private static void ShowProcessInfo()
        {
            //DONE: Mostrar informações do processo

            try
            {
                Communication.Send($@"Processo: {ProcessInfo.ProcessName}
Executável: {ProcessInfo.StartInfo.FileName}
Tempo de execução: {DateTime.Now.Subtract(ProcessInfo.StartTime)}
Pico de WorkingSet: {ProcessInfo.PeakWorkingSet64.ToString("N0")} B", false, Communication.CommunicationType.Dialog);
            }
            catch (Exception e)
            {
                Communication.Send("Erro ao mostrar informações do processo.\n\n" + e.Message, true, Communication.CommunicationType.Dialog);
            }

        }
    }
}
