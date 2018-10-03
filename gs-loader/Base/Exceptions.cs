using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader.Base
{
    public static class Exceptions
    {
        const string exc = "Except";

        public static void Install()
        {
            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
        }

        public static void HandleThreadException(Exception exception)
        {
            TratarExceção(exception, "ThreadException");
        }

        public static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            TratarExceção(e.ExceptionObject as Exception, "UnhandledException" + (e.IsTerminating ? ": TERMINATING" : ""));
        }

        private static void TratarExceção(Exception e, string mensagemExtra)
        {
            string msg = $"{mensagemExtra}\n\nOcorreu uma exceção inesperada no sistema!\n\n{e.GetType()}\n{e.Message}\n";
            if (e.InnerException != null)
                msg = msg + $"Exceção interna: {e.InnerException.GetType()}:{e.InnerException.Message}\n";
            LogExcecao(e, exc, mensagemExtra);
            Dialog.Error(msg);
        }

        /// <summary>
        /// Registra informações de uma exceção no log
        /// </summary>
        /// <param name="e">Exceção</param>
        /// <param name="bloco">Texto gravado no tipo de registro de log</param>
        /// <param name="mensagemExtra">Mensagem registrada antes da exceção</param>
        public static void LogExcecao(Exception e, string bloco, string mensagemExtra = null)
        {
            mensagemExtra = mensagemExtra ?? "EXCEÇÃO";
            if (string.IsNullOrEmpty(bloco))
                bloco = "EXCEPT";

            Log.Add("*** " + mensagemExtra, bloco);
            int nivel = 0;
            while (e != null)
            {
                nivel++;
                string ident = new string(' ', nivel * 4);
                Log.Add($"{ident}{e.GetType()}: {e.Message}", bloco);
                Log.Add($"{ident}  CS {e.StackTrace}", bloco);
                e = e.InnerException;
            }
            Log.Add("***", bloco);
        }
    }
}
