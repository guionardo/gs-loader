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
            //TODO: Verificar se o setupfile é um arquivo existente e executável

            Console.WriteLine("RUN: " + setupFile ?? "SEM SETUP");
        }
    }
}
