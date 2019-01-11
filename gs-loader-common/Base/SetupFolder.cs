using gs_loader_common.Resources;
using System;
using System.IO;

namespace gs_loader_common.Base
{
    /// <summary>
    /// Classe para controle da pasta de setup
    /// </summary>
    public class SetupFolder
    {
        #region Paths
        /// <summary>
        /// Local da aplicação
        /// </summary>
        public readonly string AppPath;

        /// <summary>
        /// Local da pasta de backup (.gsloader/backup)
        /// </summary>
        public readonly string BackupPath;

        /// <summary>
        /// Local da pasta de cache (.gsloader/cache)
        /// </summary>
        public readonly string CachePath;

        /// <summary>
        /// Local do arquivo de banco de dados (.gsloader/stats/stats.db)
        /// </summary>
        public readonly string DBPath;

        /// <summary>
        /// Local do log de sistema (.gsloader/log)
        /// </summary>
        public readonly string LogPath;

        /// <summary>
        /// Local da pasta de setup (.gsloader)
        /// </summary>
        public readonly string SetupPath;

        /// <summary>
        /// Local da pasta de estatísticasd (.gsloader/stats)
        /// </summary>
        public readonly string StatsPath;

        /// <summary>
        /// Local da pasta de telemetria (.gsloader/telemetry)y
        /// </summary>
        public readonly string TelemetryPath;
        #endregion

        private static readonly string fileReadMe;

        static SetupFolder()
        {
            fileReadMe = Strings.Get(StringName.FileReadMe);
        }
        /// <summary>
        /// Inicializa pasta de setup
        /// </summary>
        /// <param name="folder">pasta da aplicação</param>
        public SetupFolder(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                throw new ArgumentNullException(folder, "Pasta da aplicação deve ser informada");

            if (!Directory.Exists(folder))
                throw new DirectoryNotFoundException(Strings.Get(StringName.DirectoryNotFound, "DIR", folder));

            AppPath = Path.GetFullPath(folder);
            SetupPath = Path.Combine(AppPath, ".gsloader");
            TelemetryPath = Path.Combine(AppPath, "telemetry");
            BackupPath = Path.Combine(SetupPath, "backup");
            StatsPath = Path.Combine(SetupPath, "stats");
            DBPath = Path.Combine(StatsPath, "stats.db");
            LogPath = Path.Combine(SetupPath, "log");
            CachePath = Path.Combine(SetupPath, "cache");

            foreach (var f in new string[] { SetupPath, CachePath, BackupPath, StatsPath, LogPath, TelemetryPath })
                if (!IO.MakeFolder(f))
                    throw new DirectoryNotFoundException(
                        Strings.Get(StringName.DirectoryNotCreated,
                        "DIR", f, "ERROR", IO.LastError),
                        IO.LastException);

            Log.SetLogPath(LogPath);

            var fa = File.GetAttributes(SetupPath);
            if (!fa.HasFlag(FileAttributes.Hidden))
                File.SetAttributes(SetupPath, fa | FileAttributes.Hidden);

            try
            {
                File.WriteAllText(Path.Combine(SetupPath, fileReadMe), Properties.Resources.setupReadme);
            }
            catch (Exception e)
            {
                throw new Exception(Strings.Get(StringName.FileNotCreated, "FILE", fileReadMe, "ERROR", e.Message), e);
            }
        }

    }
}
