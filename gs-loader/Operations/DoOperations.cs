using gs_loader_common.Base;
using gs_loader_common.Forms;
using gs_loader.Run;
using gs_loader_common.Setup;
using gs_loader_common.Stats;
using System;
using System.IO;
using System.Text;
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
            setupFile = SetupData.ParseFileName(setupFile);
            if (!File.Exists(setupFile))
                SetupData.CreateEmptySetupFile(setupFile);
            if (!SetupData.Read(setupFile, out SetupData _setupData, out string msg))
                return null;

            return new SetupForm(_setupData);
        }

        /// <summary>
        /// Verifica os arquivos na pasta com os dados do SetupData
        /// </summary>
        /// <param name="setupFile"></param>
        internal static void Verify(string setupFile)
        {
            setupFile = SetupData.ParseFileName(setupFile);
            if (!File.Exists(setupFile))
            {
                //DONE: Gerar retorno quando o arquivo de setup não existir
                Output.SingleMessage("Arquivo de setup inexistente [" + setupFile + "]", true, 1);
                return;
            }

            if (!SetupData.Read(setupFile, out SetupData _setupData, out string msg))
            {
                //DONE: Gerar retorno quando o arquivo de setup não puder ser aberto
                Output.SingleMessage("Arquivo de setup [" + setupFile + "]:\n" + msg, true, 1);
                return;
            }

            if (SetupVerify.Verify(_setupData, Path.GetDirectoryName(setupFile), out msg))
            {
                //DONE: Gerar retorno quando os arquivos forem válidos
                Output.SingleMessage("Setup do sistema é válido [" + setupFile + "]", false, 0);
            }
            else
            {
                //DONE: Gerar retorno quando os arquivos não forem válidos
                Output.SingleMessage("Setup do sistema [" + setupFile + "]:\n" + msg, true, 1);
            }

        }

        internal static void Stats(string setupFile)
        {
            setupFile = SetupData.ParseFileName(setupFile);
            if (!File.Exists(setupFile))
            {
                //DONE: Gerar retorno quando o arquivo de setup não existir
                Output.SingleMessage("Arquivo de setup inexistente [" + setupFile + "]", true, 1);
                return;
            }

            if (!SetupData.Read(setupFile, out SetupData _setupData, out string msg))
            {
                //DONE: Gerar retorno quando o arquivo de setup não puder ser aberto
                Output.SingleMessage("Arquivo de setup [" + setupFile + "]:\n" + msg, true, 1);
                return;
            }


            System.Collections.Generic.List<ProcessInstance> processInstances = null;
            if (DoStats.ListInstances(_setupData.Executable.File, ref processInstances))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Estatísticas: " + _setupData.Executable.File);
                sb.AppendLine("Número de execuções: " + processInstances.Count);
                foreach (var pi in processInstances)
                {
                    sb.AppendLine(pi.ToString());
                }
                Output.MultipleMessage(sb.ToString(), false, 0);
                Arguments.TreatArguments.OperationForm = OutputMultipleMessage.CurrentForm;
            }

        }
    }
}
