using System;
using System.IO;

namespace gs_loader.Base
{
    public class IO
    {
        public static string LastError { get; private set; }

        public static bool MakeFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName)) return false;
            if (Directory.Exists(folderName))
                return true;

            try
            {
                Directory.CreateDirectory(folderName);
                LastError = "";
            }
            catch (Exception e) { LastError = e.Message; }
            return Directory.Exists(folderName);
        }

        public static bool TryDelete(string fileName)
        {
            if (!File.Exists(fileName))
                return true;
            try
            {
                File.Delete(fileName);
                LastError = "";
            }
            catch (Exception e)
            {
                LastError = e.Message;
            }
            return !File.Exists(fileName);
        }

        public static bool TryMove(string fileName, string destiny)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(destiny) || !File.Exists(fileName))
                return false;

            try
            {
                TryDelete(destiny);
                File.Move(fileName, destiny);
                LastError = "";
            }
            catch (Exception e)
            {
                LastError = e.Message;
            }
            return File.Exists(destiny) && string.IsNullOrEmpty(LastError);
        }   
    }
}
