using gs_loader.Forms;
using System;

namespace gs_loader.Base
{
    public static class Output
    {
        /// <summary>
        /// Variável de controle de saída: True = informações retornando via console
        /// </summary>
        public static bool NoGUI { get; set; } = false;

        /// <summary>
        /// Código de saída da aplicação
        /// </summary>
        public static int ExitCode { get; set; } = 0;

        /// <summary>
        /// Apresenta mensagem simples
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exitCode"></param>
        public static void SingleMessage(string message, bool error = false, int exitCode = -1)
        {
            if (exitCode >= 0)
                ExitCode = exitCode;
            if (NoGUI)
            {
                Console.WriteLine((error ? "ERRO:" : "") + message);
            }
            else
            {
                if (error)
                    Dialog.Error(message);
                else
                    Dialog.Message(message);
            }
        }

        public static void MultipleMessage(string message, bool error=false,int exitCode = -1)
        {
            if (exitCode >= 0)
                ExitCode = exitCode;
            if (NoGUI)
            {
                if (error)
                    Console.WriteLine("*** ERRO ***");
                Console.WriteLine(message);
            }
            else
            {
                OutputMultipleMessage.Mostrar(message, error);
            }
        }
    }
}
