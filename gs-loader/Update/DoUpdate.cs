using gs_loader.Base;
using gs_loader.Setup;
using Ionic.Zip;
using System;
using System.IO;

namespace gs_loader.Update
{
    public class DoUpdate
    {
        /// <summary>
        /// Verifica se os arquivos do cache existem e tem a mesma característica de setupData
        /// </summary>
        /// <param name="setupData"></param>
        /// <returns></returns>
        public static bool CheckCache(SetupData setupData)
        {
            string cache = IO.CacheFolder(setupData, true);
            if (string.IsNullOrEmpty(cache) || !Directory.Exists(cache))
                return false;

            if (!CheckCacheFile(setupData.Executable, cache, out string message))
            {
                return false;
            }

            foreach (var f in setupData.Files)
                if (!CheckCacheFile(f, cache, out message))
                    return false;
            //TODO: Verificar se todos os arquivos listados em setupData existem e tem as mesmas características na pasta cache

            return true;

        }

        public static bool CheckWritable(SetupData setupdata, string localFolder)
        {
            if (string.IsNullOrEmpty(localFolder) || !Directory.Exists(localFolder))
                return false;

            if (!CheckWritableFile(setupdata.Executable, localFolder, out string message))
                return false;

            foreach (var f in setupdata.Files)
                if (!CheckWritableFile(f, localFolder, out message))
                    return false;

            return true;
        }

        public static bool Backup(SetupData setupData, string localFolder)
        {
            if (string.IsNullOrEmpty(localFolder) || !Directory.Exists(localFolder))
                return false;

            var backupFolder = IO.BackupFolder(setupData, localFolder);
            if (string.IsNullOrEmpty(backupFolder) || !Directory.Exists(backupFolder))
                return false;

            var backupFile = Path.Combine(backupFolder, DateTime.Today.ToString("yyyyMMdd") + "_" + setupData.Executable.Version.ToString() + ".zip");
            StringWriter zipLog = new StringWriter();
            bool success = false;
            bool exception = false;
            var zip = new ZipFile
            {
                CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression,
                StatusMessageTextWriter = zipLog,
                ZipErrorAction = ZipErrorAction.InvokeErrorEvent
            };
            zip.ZipError += (object sender, ZipErrorEventArgs e) =>
            {
                zipLog.WriteLine("ERROR: " + e.ArchiveName + " " + e.Exception.Message);
                exception = true;
            };
            zip.SaveProgress += (object sender, SaveProgressEventArgs e) =>
            {
                switch (e.EventType)
                {
                    case ZipProgressEventType.Adding_Started:
                    case ZipProgressEventType.Adding_Completed:
                    case ZipProgressEventType.Saving_Started:
                        zipLog.WriteLine(e.EventType);
                        break;
                    case ZipProgressEventType.Saving_Completed:
                        zipLog.WriteLine(e.EventType);
                        success = true;
                        break;
                    case ZipProgressEventType.Error_Saving:
                        zipLog.WriteLine(e.EventType + " " + e.CurrentEntry.FileName);
                        exception = true;
                        break;
                }
            };
            zip.UpdateFile(Path.Combine(localFolder, setupData.Executable.File), "\\");
            foreach (var f in setupData.Files)
                zip.UpdateFile(Path.Combine(localFolder, f.Folder, f.File), f.Folder);


            zip.Save(backupFile);

            Log.Add(zipLog.ToString() + "\nSaved to " + backupFile, "BACKUP");

            return success && !exception;
        }

        /// <summary>
        /// Compara o arquivo do cache com a informação de SetupFile
        /// </summary>
        /// <param name="setupFile"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static bool CheckCacheFile(SetupFile setupFile, string cache, out string message)
        {
            string file = Path.Combine(cache, setupFile.Folder, setupFile.File);
            if (!File.Exists(file))
            {
                message = "Arquivo " + file + " inexistente";
                return false;
            }
            FileInfo fi = new FileInfo(file);
            if (fi.Length != setupFile.Size)
            {
                message = "Tamanho " + fi.Length + " diferente do esperado " + setupFile.Size;
                return false;
            }
            var md5 = IO.MD5(file);
            if (!setupFile.MD5.Equals(md5, StringComparison.InvariantCultureIgnoreCase))
            {
                message = "MD5 " + md5 + " diferente do esperado " + setupFile.MD5;
                return false;
            }

            message = "OK";
            return true;
        }

        /// <summary>
        /// Verifica se um arquivo tem permissão de escrita para este usuário
        /// </summary>
        /// <param name="setupFile"></param>
        /// <param name="localFolder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static bool CheckWritableFile(SetupFile setupFile, string localFolder, out string message)
        {
            string file = Path.Combine(localFolder, setupFile.Folder, setupFile.File);
            if (!File.Exists(file))
            {
                message = "Arquivo inexistente";
                return true;
            }
            try
            {
                using (var f = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    message = "Arquivo com permissão de escrita";
                    return true;
                }
            }
            catch (Exception e)
            {
                message = "Arquivo sem permissão de escrita: " + e.Message;
                return false;
            }
        }
    }
}
