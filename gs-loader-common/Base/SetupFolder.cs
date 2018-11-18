using System;
using System.IO;

namespace gs_loader_common.Base
{
    /// <summary>
    /// Classe para controle da pasta de setup
    /// </summary>
    public class SetupFolder
    {
        /// <summary>
        /// Local da aplicação
        /// </summary>
        public readonly string AppPath;

        /// <summary>
        /// Local da pasta de setup (.gsloader)
        /// </summary>
        public readonly string SetupPath;

        private const string fileReadMe = "ATENÇÃO-LEIA-ME.TXT";

        /// <summary>
        /// Inicializa pasta de setup
        /// </summary>
        /// <param name="folder">pasta da aplicação</param>
        public SetupFolder(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                throw new ArgumentNullException(folder, "Pasta da aplicação deve ser informada");

            if (!Directory.Exists(folder))
                throw new DirectoryNotFoundException("Pasta da aplicação inexistente (" + folder + ")");

            AppPath = Path.GetFullPath(folder);
            SetupPath = Path.Combine(AppPath, ".gsloader");
            CachePath = Path.Combine(SetupPath, "cache");
            BackupPath = Path.Combine(SetupPath, "backup");
            StatsPath = Path.Combine(SetupPath, "stats");
            DBPath = Path.Combine(StatsPath, "stats.db");
            LogPath = Path.Combine(SetupPath, "log");

            if (!IO.MakeFolder(SetupPath))
                throw new DirectoryNotFoundException("Erro ao criar pasta de setup:" + IO.LastError, IO.LastException);
            if (!IO.MakeFolder(CachePath))
                throw new DirectoryNotFoundException("Erro ao criar pasta de cache:" + IO.LastError, IO.LastException);
            if (!IO.MakeFolder(BackupPath))
                throw new DirectoryNotFoundException("Erro ao criar pasta de backup:" + IO.LastError, IO.LastException);
            if (!IO.MakeFolder(StatsPath))
                throw new DirectoryNotFoundException("Erro ao criar pasta de estatísticas:" + IO.LastError, IO.LastException);
            if (!IO.MakeFolder(LogPath))
                throw new DirectoryNotFoundException("Erro ao criar pasta de log:" + IO.LastError, IO.LastException);

            var fa = File.GetAttributes(SetupPath);
            if (!fa.HasFlag(FileAttributes.Hidden))
                File.SetAttributes(SetupPath, fa | FileAttributes.Hidden);

            try
            {
                File.WriteAllText(Path.Combine(SetupPath, fileReadMe), Properties.Resources.setupReadme);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao criar arquivo {fileReadMe} na pasta de setup:" + e.Message, e);
            }
        }
        /// <summary>
        /// Local da pasta de backup (.gsloader/backup)
        /// </summary>
        public string BackupPath { get; }

        /// <summary>
        /// Local da pasta de cache (.gsloader/cache)
        /// </summary>
        public string CachePath { get; }
        /// <summary>
        /// Local do arquivo de banco de dados (.gsloader/stats/stats.db)
        /// </summary>
        public string DBPath { get; }
        public string LogPath { get; }

        /// <summary>
        /// Local da pasta de estatísticasd (.gsloader/stats)
        /// </summary>
        public string StatsPath { get; }
    }
}
