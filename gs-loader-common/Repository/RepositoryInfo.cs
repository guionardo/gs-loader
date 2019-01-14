using System;
using System.IO;
using System.Text.RegularExpressions;
using gs_loader_common.Base;
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
        private string _telemetryHost = "";
        public Program[] Programs { get; private set; }
        /// <summary>
        /// Inicializa uma instância do repositório a partir de arquivo de configuração
        /// </summary>
        /// <param name="fileName"></param>
        public RepositoryInfo(string repositoryHost = null)
        {
            if (!string.IsNullOrEmpty(repositoryHost))
            {
                if (Directory.Exists(repositoryHost))
                {
                    repositoryHost = @"file://" + repositoryHost;
                }

                if (Regex.IsMatch(repositoryHost, @"(http|https):\/\/(.*)"))
                {
                    _repositoryHost = repositoryHost;
                    RepositoryType = RepositoryType.HTTP;
                    ReadRepositoryFromHTTP(_repositoryHost, true);
                }
                else if (Regex.IsMatch(repositoryHost, @"(file):\/\/(.*)"))
                {
                    _repositoryHost = repositoryHost.Substring(7);
                    RepositoryType = RepositoryType.Folder;
                    if (TelemetryHostType == TelemetryHostType.None)
                        TelemetryHost = @"(file)://" + _repositoryHost + "/telemetry";

                    ReadRepositoryFromFile(Path.ChangeExtension(Path.GetFullPath(_repositoryHost), "json"), true);
                }
                else
                {
                    _repositoryHost = "";
                    RepositoryType = RepositoryType.None;
                }

            }

        }

        public IRepository CreateRepository()
        {
            switch (RepositoryType)
            {
                case RepositoryType.None:
                    return null;
                case RepositoryType.Folder:
                    return new FolderRepository(_repositoryHost);
                case RepositoryType.HTTP:
                    return new HTTPRepository(_repositoryHost);
            }
            return null;
        }

        private void ReadRepositoryFromHTTP(string repositoryHost, bool noExceptions = false)
        {
            //TODO: Carregar informações do repositório HTTP            
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

        /// <summary>
        /// Nome do proprietário do repositório
        /// </summary>
        public string Proprietary { get; set; } = "";

        /// <summary>
        /// Host do repositório
        /// http://repositorio.com/folder
        /// file://pasta/subpasta 
        /// </summary>
        public string RepositoryHost
        {
            get => _repositoryHost; set
            {
                if (Regex.IsMatch(value, @"(http|https):\/\/(.*)"))
                {
                    _repositoryHost = value;
                    RepositoryType = RepositoryType.HTTP;
                }
                else if (Regex.IsMatch(value, @"(file):\/\/(.*)"))
                {
                    _repositoryHost = value.Substring(7);
                    RepositoryType = RepositoryType.Folder;
                    if (TelemetryHostType == TelemetryHostType.None)
                        TelemetryHost = @"(file):\/\/" + _repositoryHost + "/telemetry";
                }
                else
                {
                    _repositoryHost = "";
                    RepositoryType = RepositoryType.None;
                }
            }
        }

        /// <summary>
        /// Nome do repositório
        /// </summary>
        public string RepositoryName { get; set; } = "";

        /// <summary>
        /// Tipo do repositório (http / arquivo)
        /// </summary>
        public RepositoryType RepositoryType { get; private set; } = RepositoryType.None;
        /// <summary>
        /// Host de telemetria 
        /// </summary>
        public string TelemetryHost
        {
            get { return _telemetryHost; }
            set
            {
                if (Regex.IsMatch(value, @"(http|https):\/\/(.*)"))
                {
                    _telemetryHost = value;
                    TelemetryHostType = TelemetryHostType.HTTP;
                }
                else if (Regex.IsMatch(value, @"(file):\/\/(.*)"))
                {
                    _telemetryHost = value.Substring(7);
                    TelemetryHostType = TelemetryHostType.LogFile;
                }
                else
                {
                    _telemetryHost = "";
                    TelemetryHostType = TelemetryHostType.None;
                }
            }
        }

        public TelemetryHostType TelemetryHostType { get; private set; } = TelemetryHostType.None;
        public bool ReadProgram(string programName, out string jsonInfo)
        {
            jsonInfo = "";

            switch (RepositoryType)
            {
                case RepositoryType.None:
                    return false;
                case RepositoryType.HTTP:
                    return ReadProgramFromHTTP(programName, out jsonInfo);
                case RepositoryType.Folder:
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
            if (string.IsNullOrEmpty(_telemetryHost) || TelemetryHostType == TelemetryHostType.None)
                return false;

            switch (TelemetryHostType)
            {
                case TelemetryHostType.HTTP:
                    return SendTelemetryToHTTP(telemetryMessage);
                case TelemetryHostType.LogFile:
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