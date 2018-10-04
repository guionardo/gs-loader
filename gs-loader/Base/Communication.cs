namespace gs_loader.Base
{
    public class Communication
    {
        public static bool Console = false;

        public static void Send(string message, bool error = false)
        {
            if (Console)
            {
                System.Console.WriteLine(message);
                return;
            }
            if (error)
                Dialog.Error(message);
            else
                Dialog.Message(message);
        }
    }
}
