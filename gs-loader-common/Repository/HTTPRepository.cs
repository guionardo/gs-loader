using gs_loader_common.Interfaces;
using gs_loader_common.Programs;
using gs_loader_common.Telemetry;
using System;
using System.Collections.Generic;

namespace gs_loader_common.Repository
{
    public class HTTPRepository : IRepository
    {
        string RepositoryHost;

        public HTTPRepository(string repositoryHost)
        {
            RepositoryHost = repositoryHost;
        }

        public bool CheckRepository(out string message)
        {
            throw new NotImplementedException();
        }

        public bool DownloadToCache(string programName, string cacheFolder)
        {
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
    }
}
