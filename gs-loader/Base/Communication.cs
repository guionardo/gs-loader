using gs_loader.Forms;
using System;

namespace gs_loader.Base
{
    public class Communication
    {

        public enum CommunicationType
        {
            Console,
            Dialog,
            Notify
        }

        public static void Send(string message, bool error, CommunicationType type)
        {
            switch (type)
            {
                case CommunicationType.Console:
                    Console.WriteLine((error ? "ERROR: " : "") + message);
                    break;
                case CommunicationType.Dialog:
                    if (error)
                        Dialog.Error(message);
                    else
                        Dialog.Message(message);
                    break;
                case CommunicationType.Notify:
                    if (error)
                        NotifyLoader.UpdateIcon(UpdateIconType.ShowBalloonError, message);
                    else
                        NotifyLoader.UpdateIcon(UpdateIconType.ShowBaloonInfo, message);
                    break;
            }
        }
    }
}
