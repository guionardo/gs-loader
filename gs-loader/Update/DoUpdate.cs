using gs_loader.Base;
using gs_loader.Setup;
using Ionic.Zip;
using System;
using System.IO;

namespace gs_loader.Update
{
    public class DoUpdate
    {
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
        /// Verifica se os arquivos do cache existem e tem a mesma característica de setupData
        /// </summary>
        /// <param name="setupData"></param>
        /// <returns></returns>
        public static bool CheckCache(SetupData setupData)
        {
            string cache = IO.CacheFolder(setupData, true);
            if (string.IsNullOrEmpty(cache) || !Directory.Exists(cache))
                return false;

            //DONE: Verificar se todos os arquivos listados em setupData existem e tem as mesmas características na pasta cache

            return SetupVerify.Verify(setupData, cache, out string message);

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
