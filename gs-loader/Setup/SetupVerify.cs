using gs_loader.Base;
using System;
using System.IO;

namespace gs_loader.Setup
{
    public class SetupVerify
    {
        /// <summary>
        /// Verificar se os arquivos de um setup estão de acordo com os dados do metadata
        /// </summary>
        /// <param name="setupData"></param>
        /// <param name="targetFolder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Verify(SetupData setupData, string targetFolder, out string message)
        {
            if (!Directory.Exists(targetFolder))
            {
                message = "Pasta " + (targetFolder ?? "<não informada>") + " inexistente";
                return false;
            }
            if (setupData.Executable == null)
            {
                message = "Executável principal não informado";
                return false;
            }
            string file = Path.Combine(targetFolder, setupData.Executable.File);
            if (!File.Exists(file))
            {
                message = "Executável principal inexistente (" + file + ")";
                return false;
            }

            if (!VerifyFile(setupData.Executable,targetFolder,out message))
            {             
                return false;
            }
            if (setupData.Files != null)
                foreach (var f in setupData.Files)
                    if (!VerifyFile(f, targetFolder, out message))
                        return false;

            message = "OK";
            return true;
        }

        /// <summary>
        /// Compara o arquivo do cache com a informação de SetupFile
        /// </summary>
        /// <param name="setupFile"></param>
        /// <param name="targetFolder"></param>
        /// <returns></returns>
        private static bool VerifyFile(SetupFile setupFile, string targetFolder, out string message)
        {
            string file = Path.Combine(targetFolder, setupFile.Folder, setupFile.File);
            if (!File.Exists(file))
            {
                message = "Arquivo " + file + " inexistente";
                return false;
            }
            FileInfo fi = new FileInfo(file);
            if (fi.Length != setupFile.Size)
            {
                message = "Tamanho " + fi.Length + " diferente do esperado " + setupFile.Size;
                return false;
            }
            var md5 = IO.MD5(file);
            if (!setupFile.MD5.Equals(md5, StringComparison.InvariantCultureIgnoreCase))
            {
                message = "MD5 " + md5 + " diferente do esperado " + setupFile.MD5;
                return false;
            }

            message = "OK";
            return true;
        }

    }
}
