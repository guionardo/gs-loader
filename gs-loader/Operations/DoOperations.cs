using gs_loader.Base;
using gs_loader.Forms;
using gs_loader.Run;
using gs_loader.Setup;
using System;
using System.IO;
using System.Windows.Forms;

namespace gs_loader.Operations
{
    public static class DoOperations
    {


        public static Form ShowHelp(string content)
        {
            string exe = "null"; // Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);
            content = $@"
Aplicação de carregamento e atualização de sistemas
Guiosoft Informática

Utilização: {exe} [opções]

" + (content ?? "");

            return new HelpForm(content);

            //TODO: Apresentar o Help

        }

        internal static bool Run(string setupFile)
        {
            if (string.IsNullOrEmpty(setupFile))
                setupFile = Environment.CurrentDirectory;

            setupFile = SetupData.ParseFileName(setupFile);
            if (!File.Exists(setupFile))
            {
                Communication.Send("Arquivo de setup inexistente: " + setupFile, true, Communication.CommunicationType.Dialog);
                return false;
            }
            if (!SetupData.Read(setupFile, out SetupData setupData, out string message))
            {
                //DONE: Run: Exibir mensagem de erro ao ler o arquivo setup
                Communication.Send("Erro na leitura do arquivo de setup: " + message, true, Communication.CommunicationType.Dialog);
                return false;
            }

            if (!DoRun.Run(setupData, Path.GetDirectoryName(setupFile), out message))
            {
                Communication.Send(message, true, Communication.CommunicationType.Notify);
                //DONE: Run: Exibir mensagem de erro
                return false;
            }

            return true;
        }

        internal static Form Setup(string setupFile)
        {
            SetupData _setupData;
            setupFile = SetupData.ParseFileName(setupFile);
            if (!File.Exists(setupFile))
                SetupData.CreateEmptySetupFile(setupFile);
            if (!SetupData.Read(setupFile, out _setupData, out string msg))
                return null;

            return new SetupForm(_setupData);
        }

    }
}
