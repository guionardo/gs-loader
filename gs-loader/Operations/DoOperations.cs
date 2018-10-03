using gs_loader.Run;
using gs_loader.Setup;
using System;
using System.IO;

namespace gs_loader.Operations
{
    public static class DoOperations
    {
        public static void ShowHelp(string content)
        {
            string exe = "null"; // Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location);
            Console.WriteLine($@"
Aplicação de transmissão e consulta de dados do BlocoX - PAF/ECF - SC
Time Informática Ltda

Utilização: {exe} [opções]

");
            Console.WriteLine(content);
            //TODO: Apresentar o Help           

        }

        internal static void Setup()
        {
            throw new NotImplementedException();
        }

        internal static void Run(string setupFile)
        {
            if (string.IsNullOrEmpty(setupFile))
                setupFile = Environment.CurrentDirectory;

            setupFile = SetupData.ParseFileName(setupFile);
            if (!File.Exists(setupFile))
            {
                //TODO: Verificar se o setupfile é um arquivo existente e executável
                return;
            }
            if (!SetupData.Read(setupFile, out SetupData setupData, out string message))
            {
                //TODO: Exibir mensagem de erro ao ler o arquivo setup
                return;
            }
            if (!DoRun.Run(setupData, Path.GetDirectoryName(setupFile), out message))
            {
                //TODO: Exibir mensagem de erro
            }
        }
    }
}
