using System;
using System.IO;
using gs_loader_common.Base;
using gs_loader_common.Hosts;
using gs_loader_common.Interfaces;
using gs_loader_common.Programs;
using gs_loader_common.Telemetry;
using Newtonsoft.Json;

namespace gs_loader_common.Repository
{
    /// <summary>
    /// Informações de repositório (armazenadas no arquivo repository.json)
    /// </summary>
    public class RepositoryInfo : Assignable
    {
        private string _repositoryHost;
        private string _telemetryHost;
        /// <summary>
        /// Inicializa uma instância do repositório a partir de arquivo de configuração
        /// </summary>
        /// <param name="fileName"></param>
        public RepositoryInfo(string repositoryHost = null)
        {
            _repositoryHost = "";
            RepositoryHost = null;
            TelemetryHost = null;

            if (!string.IsNullOrEmpty(repositoryHost))
                RepositoryName = repositoryHost;
        }

        public Program[] Programs { get; private set; }
        /// <summary>
        /// Nome do proprietário do repositório
        /// </summary>
        public string Proprietary { get; set; } = "";

        public Host RepositoryHost { get; set; }

        /// <summary>
        /// Host do repositório
        /// http://repositorio.com/folder
        /// file://pasta/subpasta 
        /// </summary>
        public string RepositoryName
        {
            get => RepositoryHost == null ? "" : RepositoryHost.HostName;
            set
            {
                RepositoryHost = Host.FromName(value);
                switch (RepositoryHost.HostType)
                {
                    case HostType.HTTP:
                        TelemetryHost = RepositoryHost;

                        break;
                    case HostType.LocalFolder:
                    case HostType.SharedFolder:
                        if (TelemetryHostType == HostType.None)
                        {
                            TelemetryHost = Host.FromName(value + "\\telemetry");
                            _telemetryHost = value + "\\telemetry";
                        }
                        break;


                    default:
                        TelemetryHost = null;
                        break;


                }
            }
        }

        /// <summary>
        /// Tipo do Host do Repositório
        /// </summary>
        public HostType RepositoryType => RepositoryHost == null ? HostType.None : RepositoryHost.HostType;

        public Host TelemetryHost { get; set; }

        /// <summary>
        /// Tipo do Host de Telemetria
        /// </summary>
        public HostType TelemetryHostType => TelemetryHost == null ? HostType.None : TelemetryHost.HostType;

        public IRepository CreateRepository()
        {
            switch (RepositoryType)
            {
                case HostType.None:
                    return null;
                case HostType.LocalFolder:
                case HostType.SharedFolder:
                    return new FolderRepository(RepositoryName);
                case HostType.HTTP:
                    return new HTTPRepository(RepositoryName);
            }
            return null;
        }

        public bool ReadProgram(string programName, out string jsonInfo)
        {
            jsonInfo = "";

            switch (RepositoryType)
            {
                case HostType.None:
                    return false;
                case HostType.HTTP:
                    return ReadProgramFromHTTP(programName, out jsonInfo);
                case HostType.LocalFolder:
                case HostType.SharedFolder:
                    return ReadProgramFromFolder(programName, out jsonInfo);
            }
            return false;
        }

        /// <summary>
        /// Grava informações do repositório em um arquivo .json
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool Save(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            fileName = Path.ChangeExtension(Path.GetFullPath(fileName), "json");

            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(fileName, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendTelemetry(TelemetryMessage telemetryMessage)
        {
            if (string.IsNullOrEmpty(_telemetryHost) || TelemetryHostType == HostType.None)
                return false;

            switch (TelemetryHostType)
            {
                case HostType.HTTP:
                    return SendTelemetryToHTTP(telemetryMessage);
                case HostType.LocalFolder:
                case HostType.SharedFolder:
                    return SendTelemetryToFolder(telemetryMessage);
            }

            return false;
        }

        public override string ToString()
        {
            return (RepositoryName ?? "NONAME") + " " + (Proprietary ?? "NOPROPRIETARY");
        }

        private bool ReadProgramFromFolder(string programName, out string jsonInfo)
        {
            //TODO: Implementar captura da informação do programa a partir de repositório pasta
            throw new NotImplementedException();
        }

        private bool ReadProgramFromHTTP(string programName, out string jsonInfo)
        {
            //TODO: Implementar captura da informação do programa a partir de repositório HTTP
            throw new NotImplementedException();
        }

        private void ReadRepositoryFromFile(string fileName, bool noExceptions = false)
        {
            if (!File.Exists(fileName))
                if (noExceptions)
                    return;
                else
                    throw new FileNotFoundException("Setup file not found", fileName);

            try
            {
                var ri = JsonConvert.DeserializeObject<RepositoryInfo>(File.ReadAllText(fileName));
                Assign(ri);
            }
            catch (Exception e)
            {
                if (!noExceptions)
                    throw new Exception("Error parsing repository setup file", e);
            }
        }

        private void ReadRepositoryFromHTTP(string repositoryHost, bool noExceptions = false)
        {
            //TODO: Carregar informações do repositório HTTP            
        }
        private bool SendTelemetryToFolder(TelemetryMessage telemetryMessage)
        {
            //TODO: Implementar envio de telemetria para pasta
            throw new NotImplementedException();
        }

        private bool SendTelemetryToHTTP(TelemetryMessage telemetryMessage)
        {
            //TODO: Implementar envio de telemetria para HTTP
            throw new NotImplementedException();
        }
    }
}