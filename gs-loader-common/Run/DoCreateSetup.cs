using gs_loader_common.Forms;
using gs_loader_common.Programs;
using gs_loader_common.Repository;
using gs_loader_common.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gs_loader_common.Run
{
    public class DoCreateSetup
    {
        public static bool CreateSetup(string originFolder, string repositoryHost, out string message)
        {            
            originFolder = originFolder ?? "";
            if (!Directory.Exists(originFolder))
            {
                message = Strings.Get(StringName.DirectoryNotFound, "DIR", originFolder);
                return false;
            }

            repositoryHost = repositoryHost ?? "";

            var ri = new RepositoryInfo(repositoryHost);
            if (ri.RepositoryType == RepositoryType.None)
            {
                message = Strings.Get(StringName.OptionErrorRepositoryType, "REPO", repositoryHost);
                return false;
            }

            string executableFile = GetExecutableFile(originFolder);
            if (string.IsNullOrEmpty(executableFile))
            {
                message = Strings.Get(StringName.ExecutableNotInformed);
                return false;
            }

            Program p = new Program(executableFile);
            p.RepositoryHost = repositoryHost;

            // Identificar restante dos arquivos

            foreach (var f in Directory.GetFiles(originFolder, "*.*", SearchOption.AllDirectories))
            {
                if (f.Equals(executableFile, StringComparison.InvariantCultureIgnoreCase))
                    continue;
                p.Files.Add(new FileEntry(f, originFolder));
            }

            
            if (EditSetup.Editar(p))
            {
                var repository = ri.CreateRepository();
                repository.PushProgram(p, originFolder);
                message = "Programa enviado ao repositório";
                return true;
            } else
            {
                message = "Edição cancelada pelo usuário";
                return false;
            }


        }

        /// <summary>
        /// Identifica um arquivo executável em uma pasta
        /// </summary>
        /// <param name="originFolder"></param>
        /// <returns></returns>
        private static string GetExecutableFile(string originFolder)
        {
            // Identifica um arquivo executável
            var exes = Directory.GetFiles(originFolder, "*.exe");
            switch (exes.Length)
            {
                case 0:
                    // Nenhum executável
                    // TODO: Opção de outros tipos de executável (.cmd, .py, .bat, etc)
                    return "";
                case 1:
                    // Um executável
                    return exes[0];
                default:
                    // Mais de um executável, usuário deve escolher
                    var fod = new OpenFileDialog()
                    {
                        CheckFileExists = true,
                        CheckPathExists = true,
                        DefaultExt = ".exe",
                        Filter = "Executável|*.exe",
                        InitialDirectory = originFolder,
                        Multiselect = false,
                        RestoreDirectory = true,
                        Title = Strings.Get(StringName.ExecutableFile)
                    };
                    if (fod.ShowDialog() == DialogResult.OK &&
                        Path.GetExtension(fod.FileName).Equals(".exe", StringComparison.InvariantCultureIgnoreCase) &&
                        Path.GetDirectoryName(fod.FileName).Equals(originFolder, StringComparison.InvariantCulture))
                    {
                        return fod.FileName;
                    }
                    return "";
            }

        }
    }
}
