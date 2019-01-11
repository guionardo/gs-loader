using gs_loader_common.Programs;
using System;
using System.IO;

namespace gs_loader_common.Run
{
    public static class DoVerify
    {/// <summary>
     /// Verifica os arquivos na pasta com os dados do SetupData
     /// </summary>
     /// <param name="setupFile"></param>
        public static bool Verify(string basePath, out string message)
        {
            message = "";

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
                success = program.Verify(basePath, out message);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return success;

            /*    setupFile = SetupData.ParseFileName(setupFile);
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
            }*/

        }
    }
}
