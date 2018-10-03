﻿using System;
using System.Diagnostics;
using System.IO;

namespace gs_loader.Setup
{
    public class SetupFile
    {
        public string File { get; set; }
        public Version Version { get; set; }
        public string Description { get; set; }
        public string MD5 { get; set; }
        public long Size { get; set; }
        public DateTime CreationTime { get; set; }
        public string Folder { get; set; }

        public SetupFile(string fileName, string baseFolder = null)
        {
            if (System.IO.File.Exists(fileName))
            {
                File = Path.GetFileName(fileName);

                if (Path.GetExtension(fileName).Equals(".exe", StringComparison.InvariantCultureIgnoreCase) ||
                    Path.GetExtension(fileName).Equals(".dll", StringComparison.InvariantCultureIgnoreCase))
                {
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(fileName);
                    Version = new Version(fvi.FileVersion ?? "0.0.0.0");
                    Description = fvi.FileDescription ?? Path.GetFileNameWithoutExtension(fileName);
                }
                else
                {
                    Version = new Version();
                    Description = Path.GetExtension(fileName).ToLower() + " FILE";
                }
                FileInfo fi = new FileInfo(fileName);
                Size = fi.Length;

                CreationTime = System.IO.File.GetCreationTime(fileName);
                var md5 = System.Security.Cryptography.MD5.Create();
                var bytesMD5 = md5.ComputeHash(System.IO.File.ReadAllBytes(fileName));
                string hashMD5 = "";
                foreach (var b in bytesMD5)
                    hashMD5 += b.ToString("X2");
                MD5 = hashMD5.ToLower();

                if (string.IsNullOrEmpty(baseFolder))
                    baseFolder = Path.GetDirectoryName(fileName);
                if (!baseFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    baseFolder += Path.DirectorySeparatorChar;

                if (fileName.StartsWith(baseFolder, StringComparison.InvariantCultureIgnoreCase))
                    if (Path.GetDirectoryName(fileName).Length > baseFolder.Length)
                        Folder = Path.GetDirectoryName(fileName).Substring(baseFolder.Length);
                    else
                        Folder = "";
                else
                    Folder = baseFolder;

            }
        }
    }
}