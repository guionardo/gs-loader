using System;
using System.Collections.Generic;
using System.IO;
using gs_loader_common.Base;
using gs_loader_common.Telemetry;
using gs_loader_common.Update;
using Newtonsoft.Json;

namespace gs_loader_common.Setup
{
    /// <summary>
    /// Dados de setup do programa a ser executado/atualizado
    /// </summary>
    public class SetupData : ICloneable
    {
        /// <summary>
        /// Arquivo de setup padrão
        /// </summary>
        public const string DefaultFileName = "gs-loader.json";
        /// <summary>
        /// Extensões padrão para criação de SetupData a partir de diretório ou executável
        /// </summary>
        public static readonly string[] DefaultExtensions = new string[] { ".exe", ".dll" };

        /// <summary>
        /// Extensões padrão para ignorar
        /// </summary>
        public static readonly string[] DefaultIgnoredExtensions = new string[] { ".bak", ".tmp", ".obj", ".xml" };

        public string[] IgnoredExtensions = DefaultIgnoredExtensions;
        public string[] IncludeExtensions = DefaultExtensions;
        /// <summary>
        /// Argumentos utilizados na linha de comando do executável
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Executável 
        /// </summary>
        [JsonIgnore]
        public SetupFile Executable
        {
            get
            {
                foreach (var f in Files)
                    if (f.Executable)
                        return f;
                // Não encontrou nenhum executável definido. Procurará pelo primeiro executável
                for (int i = 0; i < Files.Count; i++)
                    if (string.IsNullOrEmpty(Files[i].Folder) && IO.IsExecutable(Files[i].File))
                    {
                        Files[i].Executable = true;
                        return Files[i];
                    }

                return null;
            }
            set
            {
                if (!IO.IsExecutable(value.File))
                    return;
                value.Executable = true;
                for (int i = 0; i < Files.Count; i++) Files[i].Executable = false;
                for (int i = 0; i < Files.Count; i++)
                    if (Files[i].File.Equals(value.File, StringComparison.InvariantCultureIgnoreCase) &&
                        Files[i].Folder.Equals(value.Folder, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Files[i].Assign(value);
                        return;
                    }
                Files.Add(value);
            }
        }

        /// <summary>
        /// Arquivos que farão parte do setup
        /// </summary>
        public List<SetupFile> Files { get; set; } = new List<SetupFile>();

        public string HashString
        {
            get
            {
                {
                    string hash = "";
                    foreach (var f in Files)
                        hash += f.HashString();
                    return IO.MD5FromString(hash);
                }
            }
        }

        /// <summary>
        /// Indica que somente uma instância pode ser executada por vez
        /// </summary>
        public bool JustOneInstance { get; set; }

        /// <summary>
        /// Arquivo de configuração lido
        /// </summary>
        [JsonIgnore]
        public string SetupFile { get; set; }

        /// <summary>
        /// Nome do setup
        /// </summary>
        public string SetupName { get; set; }

        /// <summary>
        /// Observação do setup
        /// </summary>
        public string SetupNote { get; set; }

        public string SetupVersion
        {
            get
            {
                foreach (var f in Files)
                    if (f.Executable)
                        return f.Version.ToString();
                return "";
            }
        }
        public TelemetryHost TelemetryHost { get; set; }
        /// <summary>
        /// Fonte de atualização
        /// </summary>
        public UpdateSource UpdateSource { get; set; }

        public UpdateType UpdateType { get; set; }
        /// <summary>
        /// Cria uma instância de setup a partir de um executável ou pasta
        /// </summary>
        /// <param name="executableFile"></param>
        /// <param name="setupData"></param>
        /// <param name="message"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static bool Create(string executableFile, out SetupData setupData, out string message, string[] files = null)
        {
            if (files == null)
                files = DefaultExtensions;

            setupData = null;
            if (Directory.Exists(executableFile))
            {
                var exes = Directory.GetFiles(executableFile, "*.exe");
                switch (exes.Length)
                {
                    case 0:
                        message = "Não foram encontrados executáveis em " + executableFile;
                        return false;
                    case 1:
                        executableFile = exes[0];
                        break;
                    default:
                        bool found = false;
                        foreach (var f in exes)
                            if (Path.GetFileNameWithoutExtension(f).Equals(Path.GetFileName(executableFile), StringComparison.InvariantCultureIgnoreCase))
                            {
                                executableFile = f;
                                found = true;
                                break;
                            }
                        if (found)
                            break;
                        message = "Foram encontrados mais de um executável em " + executableFile;
                        return false;
                }
            }
            if (!File.Exists(executableFile))
            {
                message = "Executável inexistente em " + executableFile;
                return false;
            }
            string path = Path.GetDirectoryName(executableFile);
            setupData = new SetupData
            {
                Executable = new SetupFile(executableFile),
                SetupFile = Path.Combine(Path.GetDirectoryName(executableFile), DefaultFileName)
            };

            foreach (var f in files)
            {
                var f0 = Directory.GetFiles(path, f, SearchOption.AllDirectories);
                foreach (var f1 in f0)
                    if (!f1.Equals(executableFile, StringComparison.InvariantCultureIgnoreCase))
                        setupData.Files.Add(new SetupFile(f1, path));
            }


            message = "OK";
            return true;
        }

        public static bool CreateEmptySetupFile(string fileName)
        {
            fileName = ParseFileName(fileName);
            if (string.IsNullOrEmpty(fileName))
                return false;
            var sd = new SetupData();
            return Write(fileName, sd, out string message);
        }

        /// <summary>
        /// Trata o nome do arquivo caso for informado apenas a pasta
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ParseFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "";
            fileName = Path.GetFullPath(fileName);
            if (Directory.Exists(fileName))
                fileName = Path.Combine(fileName, DefaultFileName);
            return fileName;
        }

        /// <summary>
        /// Lê o setup a partir do arquivo
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="setupData"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Read(string fileName, out SetupData setupData, out string message)
        {
            setupData = null;
            message = "";
            fileName = ParseFileName(fileName);
            if (string.IsNullOrEmpty(fileName))
            {
                message = "Argumento inválido ou vazio";
                return false;
            }

            if (!File.Exists(fileName))
            {
                message = "Arquivo de setup inexistente em " + fileName;
                return false;
            }

            try
            {
                string json = File.ReadAllText(fileName);
                setupData = JsonConvert.DeserializeObject<SetupData>(json);
                setupData.SetupFile = fileName;
                message = "OK";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        /// <summary>
        /// Gravar setup no arquivo
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="setupData"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Write(string fileName, SetupData setupData, out string message)
        {
            fileName = ParseFileName(fileName);
            if (string.IsNullOrEmpty(fileName))
            {
                message = "Destino do setup inválido ou vazio";
                return false;
            }
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                message = "Caminho do arquivo de setup é inexistente (" + Path.GetDirectoryName(fileName) + ")";
                return false;
            }
            try
            {
                SetupData clone = (SetupData)setupData.Clone();
                clone.Files.Clear();
                foreach (var f in setupData.Files)
                    if (f.Include)
                        clone.Files.Add(f);

                string json = JsonConvert.SerializeObject(clone, Formatting.Indented);
                File.WriteAllText(fileName, json);
                message = "OK";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        public object Clone() => new SetupData()
        {
            Arguments = Arguments,
            Executable = (SetupFile)Executable.Clone(),
            Files = new List<SetupFile>(Files),
            JustOneInstance = JustOneInstance,
            UpdateSource = (UpdateSource)UpdateSource.Clone(),
            UpdateType = UpdateType
        };
    }
}
