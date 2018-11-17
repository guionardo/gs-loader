using gs_loader_common.Setup;
using System;
using System.IO;
using System.Text;

namespace gs_loader_common.Base
{
    public class IO
    {
        static readonly string _cacheFolder;
        /// <summary>
        /// Extensões de arquivos executáveis
        /// </summary>
        static readonly string[] executableExtensions = new string[] { "exe", "cmd", "bat", "com" };

        static IO()
        {
            _cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "GSLoader", "cache");
            if (!MakeFolder(_cacheFolder))
            {
                _cacheFolder = Path.Combine(Directory.GetCurrentDirectory(), "GSLoader", "cache");
                if (!MakeFolder(_cacheFolder))
                {
                    _cacheFolder = "";
                }
            }
        }

        public static string LastError { get; private set; } = "";
        public static Exception LastException { get; private set; } = null;

        /// <summary>
        /// Pasta de backup para as versões atualizadas
        /// </summary>
        /// <param name="setupData"></param>
        /// <param name="localFolder"></param>
        /// <returns></returns>
        public static string BackupFolder(SetupData setupData, string localFolder)
        {
            if (setupData == null ||
                setupData.Executable == null ||
                string.IsNullOrEmpty(setupData.Executable.File) ||
                string.IsNullOrEmpty(localFolder) ||
                !Directory.Exists(localFolder))
                return "";
            string backupFolder = Path.Combine(
                localFolder, "GSLoader", "Backup");
            if (MakeFolder(backupFolder))
                return backupFolder;
            return "";
        }

        /// <summary>
        /// Pasta de cache para o update
        /// </summary>
        /// <param name="setupData"></param>
        /// <param name="includeVersion"></param>
        /// <returns></returns>
        public static string CacheFolder(SetupData setupData, bool includeVersion)
        {
            if (setupData == null || setupData.Executable == null || string.IsNullOrEmpty(setupData.Executable.File))
                return "";

            string cacheFolder = Path.Combine(
                _cacheFolder,
                Math.Abs(setupData.Executable.File.ToUpperInvariant().GetHashCode()).ToString());
            if (includeVersion)
                cacheFolder = Path.Combine(cacheFolder, setupData.Executable.Version.ToString());
            return cacheFolder;
        }
        /// <summary>
        /// Retorna a data/hora de criação do arquivo em UTC e sem os milissegundos
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DateTime FileCreationTime(string fileName)
        {
            try
            {
                DateTime fct = File.GetCreationTimeUtc(fileName);
                fct.AddMilliseconds(-fct.Millisecond);
                return fct;
            }
            catch
            {
                return DateTime.MinValue;
            }

        }

        /// <summary>
        /// Identifica um arquivo executável pela sua extensão
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsExecutable(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
                foreach (var ext in executableExtensions)
                    if (Path.GetExtension(fileName).Equals("." + ext, StringComparison.InvariantCultureIgnoreCase))
                        return true;
            return false;
        }

        /// <summary>
        /// Identifica se uma extensão é de um arquivo executável
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static bool IsExecutableExtension(string extension)
        {
            if (!string.IsNullOrEmpty(extension))
            {
                if (!extension.StartsWith("."))
                    extension = "." + extension.ToLowerInvariant();
                foreach (var ext in executableExtensions)
                    if (ext.Equals(extension))
                        return true;
            }
            return false;
        }

        /// <summary>
        /// Verifica/cria pasta
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static bool MakeFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName)) return false;
            if (Directory.Exists(folderName))
                return true;

            try
            {
                Directory.CreateDirectory(folderName);
                LastError = "";
                LastException = null;
            }
            catch (Exception e) {
                LastError = e.Message;
                LastException = e;
            }
            return Directory.Exists(folderName);
        }

        public static string MD5(string fileName)
        {
            if (!File.Exists(fileName))
                return "";
            try
            {
                return MD5FromString(File.ReadAllText(fileName));
            }
            catch
            {
                return "";
            }
        }

        public static string MD5FromString(string text)
        {
            try
            {
                var md5 = System.Security.Cryptography.MD5.Create();
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                string hash = "";
                foreach (var b in bytes)
                    hash += b.ToString("X2").ToLower();
                return hash;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Tenta escluir um arquivo
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool TryDelete(string fileName)
        {
            if (!File.Exists(fileName))
                return true;
            try
            {
                File.Delete(fileName);
                LastError = "";
                LastException = null;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                LastException = e;
            }
            return !File.Exists(fileName);
        }

        /// <summary>
        /// Tenta mover um arquivo
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public static bool TryMove(string fileName, string destiny)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(destiny) || !File.Exists(fileName))
                return false;

            try
            {
                TryDelete(destiny);
                File.Move(fileName, destiny);
                LastError = "";
                LastException = null;
            }
            catch (Exception e)
            {
                LastError = e.Message;
                LastException = e;
            }
            return File.Exists(destiny) && string.IsNullOrEmpty(LastError);
        }

        /// <summary>
        /// Procura pelo executável principal na pasta
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string FindMainExecutable(string folder)
        {
            if (!Directory.Exists(folder)) return "";

            var files = Directory.GetFiles(folder, "*.exe");
            if (files.Length == 0) return "";

            foreach (var f in files)
            {
                if (Path.GetFileNameWithoutExtension(f).Equals(Path.GetFileName(folder), StringComparison.InvariantCultureIgnoreCase))
                    return f;
            }
            return "";
        }
    }
}
