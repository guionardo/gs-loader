using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using gs_loader_common.Base;
using gs_loader_common.Repository;
using gs_loader_common.Update;
using Newtonsoft.Json;

namespace gs_loader_common.Programs
{
    /// <summary>
    /// Classe para armazenar informações do programa a ser instalado/atualizado/executado
    /// </summary>
    public class Program : ICloneable
    {
        public const string DefaultFileName = "gsloader.json";
        private string _repositoryHost = "";

        private Program() { }

        /// <summary>
        /// Argumentos de execução do programa
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Arquivos que compõe a instalação
        /// </summary>
        public List<FileEntry> Files { get; private set; }

        /// <summary>
        /// Verifica se os arquivos do programa são equivalentes aos da pasta de instalação
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Verify(string basePath, out string message)
        {
            StringBuilder sb = new StringBuilder();
            if (!Main.Valid(basePath, out string msg))
                sb.AppendLine(msg);
            foreach (var f in Files)
                if (!f.Valid(basePath, out msg))
                    sb.AppendLine(msg);

            message = sb.ToString();
            return message.Length == 0;
        }

        /// <summary>
        /// Indica que somente uma instância pode ser executada por vez
        /// </summary>
        public bool JustOneInstance { get; set; }

        /// <summary>
        /// Executável principal
        /// </summary>
        public FileEntry Main { get; private set; }

        /// <summary>
        /// Observação do setup
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Nome do programa
        /// </summary>
        public string ProgramName { get; private set; }
        public string RepositoryHost
        {
            get => _repositoryHost;
            set
            {
                RepositoryInfo ri = new RepositoryInfo
                {
                    RepositoryHost = value
                };
                if (ri.RepositoryType != RepositoryType.None)
                    _repositoryHost = value;
            }
        }

        /// <summary>
        /// Tipo da atualização automática
        /// </summary>
        public UpdateType UpdateType { get; set; }
        /// <summary>
        /// Versão do programa
        /// </summary>
        public string Version { get; private set; }
        /// <summary>
        /// Carrega a partir de um arquivo .json
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public static Program FromFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Arquivo de configuração do programa não encontrado", fileName);

            try
            {
                string json = File.ReadAllText(fileName);
                Program p = FromString(json);
                return p;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar o arquivo de configuração de programa em " + fileName, e);
            }
        }

        /// <summary>
        /// Inicializa classe a partir de arquivos de uma pasta
        /// </summary>
        /// <param name="folderName">Pasta onde os arquivos de distribuição estão localizados</param>
        /// <param name="programName">Nome do programa</param>
        /// <param name="mainExecutable">Nome do executável principal (deve estar na raiz da instalação)</param>
        /// <returns></returns>
        public static Program FromFolder(string folderName, string programName, string mainExecutable = null)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentNullException("folderName");
            if (!Directory.Exists(folderName))
                throw new DirectoryNotFoundException("Pasta de distribuição não encontrada em " + folderName);
            if (string.IsNullOrEmpty(programName))
                throw new ArgumentNullException("programName");

            string baseFolder = Path.GetFileName(folderName);

            if (string.IsNullOrEmpty(mainExecutable))
            {
                foreach (var me in new string[] { baseFolder + ".cmd", baseFolder + ".bat", baseFolder + ".exe" })
                    if (File.Exists(Path.Combine(folderName, me)))
                    {
                        mainExecutable = me;
                        break;
                    }

                if (string.IsNullOrEmpty(mainExecutable))
                    throw new FileNotFoundException("Executável principal não identificado!");
            }

            mainExecutable = Path.GetFileName(mainExecutable);
            FileEntry main = new FileEntry(Path.Combine(folderName, mainExecutable), folderName);
            List<FileEntry> progfiles = new List<FileEntry>();
            try
            {
                var files = Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories);
                foreach (var f in files)
                    if (f.Equals(Path.Combine(folderName, mainExecutable))) // Ignorar o executável principal
                        continue;
                    else
                    {
                        FileEntry fe = new FileEntry(f, folderName);
                        if (fe != null)
                            progfiles.Add(fe);
                    }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar os arquivos de distribuição", e);
            }

            var p = new Program
            {
                ProgramName = programName,
                Main = main,
                Files = progfiles,
                Version = main.Version.ToString()
            };
            return p;
        }

        /// <summary>
        /// Carrega informações do programa a partir da pasta instalada
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public static Program FromInstalledFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                throw new ArgumentNullException("folderName");

            string setupPath = Path.Combine(folderName, ".gsloader");
            if (!Directory.Exists(setupPath))
                throw new DirectoryNotFoundException("Setup folder not found: " + setupPath);

            string setupFile = Path.Combine(setupPath, DefaultFileName);

            if (!File.Exists(setupFile))
                throw new FileNotFoundException("Setup file not found", setupFile);

            return FromFile(setupFile);
        }

        /// <summary>
        /// Carregar informações do programa a partir de um repositório
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="programName"></param>
        /// <returns></returns>
        public static Program FromRepository(RepositoryInfo repository, string programName)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            Program p = new Program();

            if (repository.ReadProgram(programName, out string json))
            {
                p = Program.FromString(json);
            }
            //TODO: Implementar carga de programa a partir do repositório
            throw new NotImplementedException();
        }

        public object Clone()
        {
            Program clone = new Program
            {
                Main = (FileEntry)this.Main.Clone(),
                ProgramName = this.ProgramName,
                Version = this.Version,
                Files = new List<FileEntry>()
            };
            foreach (var f in Files)
                clone.Files.Add((FileEntry)f.Clone());
            return clone;
        }

        /// <summary>
        /// Verificar a necessidade de atualização
        /// </summary>
        /// <returns></returns>
        public UpdateNeedType UpdateNeedType()
        {
            // Obter o metadata a partir do repositório
            if (string.IsNullOrEmpty(_repositoryHost))
                return Programs.UpdateNeedType.NoRepository;
            RepositoryInfo repository = new RepositoryInfo(RepositoryHost);
            if (!repository.ReadProgram(this.ProgramName, out string jsonInfo))
                // Não encontrou o programa no repositório
                return Programs.UpdateNeedType.RepositoryError;

            Program repositoryProgram = Program.FromString(jsonInfo, true);
            if (repositoryProgram == null)
                // Não foi possível ler a informação correta do programa no repositório
                return Programs.UpdateNeedType.RepositoryError;

            Base.Version vRepPro = new Base.Version(repositoryProgram.Version);

            Base.Version vPro = new Base.Version(Version);

            switch (vPro.CompareTo(vRepPro))
            {
                case 0: // Mesma versão
                case 1: // Versão atual maior do que a do repositório
                    return Programs.UpdateNeedType.Updated;
                case -1:
                    return Programs.UpdateNeedType.NeedUpdate;

            }

            return Programs.UpdateNeedType.NotSyncedFiles;

            //TODO: Implementar a checagem de arquivos não sincronizados com sua informação no arquivo gsloader.json
        }

        /// <summary>
        /// Carrega informações do programa a partir de uma string json
        /// </summary>
        /// <param name="json"></param>
        /// <exception cref="Exception">Erro de desserialização</exception>
        /// <returns></returns>
        private static Program FromString(string json, bool noExceptions = false)
        {
            try
            {
                Program p = JsonConvert.DeserializeObject<Program>(json);
                return p;
            }
            catch (Exception e)
            {
                if (noExceptions)
                    return null;
                throw new Exception("Erro ao carregar informação de configuração de programa", e);
            }
        }
    }
}
