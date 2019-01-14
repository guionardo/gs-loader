using gs_loader_common.Base;
using gs_loader_common.Interfaces;
using gs_loader_common.Programs;
using gs_loader_common.Resources;
using gs_loader_common.Telemetry;
using System;
using System.Collections.Generic;
using System.IO;

namespace gs_loader_common.Repository
{
    public class FolderRepository : IRepository
    {
        string RepositoryFile;

        string RepositoryHost;
        readonly RepositoryInfo RepositoryInfo;

        public FolderRepository(string repositoryHost)
        {
            RepositoryHost = repositoryHost;
            RepositoryFile = Path.Combine(RepositoryHost, "repository.json");
            RepositoryInfo = new RepositoryInfo(repositoryHost);
        }

        public bool CheckRepository(out string message)
        {
            // Verifica se o diretório existe
            if (!Directory.Exists(RepositoryHost))
            {
                message = Strings.Get(StringName.DirectoryNotFound, "DIR", RepositoryHost);
                return false;
            }

            // Verifica se o arquivo repository.json existe
            if (!File.Exists(RepositoryFile))
            {
                message = Strings.Get(StringName.FileNotFound, "FILE", RepositoryFile);
                return false;
            }

            string msgP = "";
            message = "";
            foreach (var r in RepositoryInfo.Programs)
                if (!r.Verify(Path.Combine(RepositoryHost, r.ProgramName), out msgP))
                    message += msgP + "\n";


            return string.IsNullOrEmpty(message);
        }

        public bool DownloadToCache(string programName, string cacheFolder)
        {
            string programPath = Path.Combine(RepositoryHost, IO.ValidFileName(programName));
            //TODO: Fazer o download dos arquivos para a pasta de cache
  //          if (IO.CopyFolderContent(Repository))
            throw new NotImplementedException();
        }

        public bool QueryPrograms(List<Program> programs)
        {
            throw new NotImplementedException();
        }


        public bool SendTelemetry(TelemetryMessage telemetryMessage)
        {
            throw new NotImplementedException();
        }

        public bool PushProgram(Program program, string setupFolder)
        {
            throw new NotImplementedException();
        }
    }
}
