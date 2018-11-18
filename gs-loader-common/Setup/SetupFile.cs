using gs_loader_common.Base;
using gs_loader_common.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace gs_loader_common.Setup
{
    public class SetupFile : ICloneable, IAssignable, IComparable
    {
        private bool _executable;

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

                CreationTime = IO.FileCreationTime(fileName);
                MD5 = IO.MD5(fileName);

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

        private SetupFile() { }

        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Nome do arquivo
        /// </summary>
        public string File { get; set; }

        public SetupFileFlags FileFlags { get; set; }
        public string Folder { get; set; }

        /// <summary>
        /// MD5
        /// </summary>
        public string MD5 { get; set; }

        public long Size { get; set; }

        /// <summary>
        /// Propriedade utilizada para identificar o estado do arquivo na grid de setup
        /// </summary>
        [JsonIgnore]
        public SetupFileState State { get; set; }
        /// <summary>
        /// Versão
        /// </summary>
        public Version Version { get; set; }

        public bool Assign(object value)
        {
            if (value is SetupFile v)
            {
                File = v.File;
                Version = (Version)v.Version.Clone();
                Description = v.Description;
                MD5 = v.MD5;
                Size = v.Size;
                CreationTime = v.CreationTime;
                Folder = v.Folder;
                FileFlags = v.FileFlags;
                return true;
            }
            return false;
        }

        public object Clone() => new SetupFile
        {
            File = File,
            Version = (Version)Version.Clone(),
            Description = Description,
            MD5 = MD5,
            Size = Size,
            CreationTime = CreationTime,
            Folder = Folder,
            FileFlags = FileFlags
        };

        public int CompareTo(object obj)
        {
            var o = (SetupFile)obj;

            int r = Folder.CompareTo(o.Folder);
            if (r == 0)
            {
                r = ExtCompare(Path.GetExtension(File), Path.GetExtension(o.File));
                if (r == 0)
                    r = File.CompareTo(o.File);
            }

            return r;
        }

        public int ExtCompare(string ext1, string ext2)
        {
            ext1 = (ext1 ?? "").ToUpperInvariant();
            ext2 = (ext2 ?? "").ToUpperInvariant();
            if (IO.IsExecutableExtension(ext1) && IO.IsExecutableExtension(ext2))
                return 0;
            if (IO.IsExecutableExtension(ext1) && !IO.IsExecutableExtension(ext2))
                return -1;
            if (!IO.IsExecutableExtension(ext1) && IO.IsExecutableExtension(ext2))
                return 1;
            return ext1.CompareTo(ext2);
        }
        public string HashString()
        {
            string hash = CreationTime.ToBinary().ToString() +
                (Description ?? "").GetHashCode().ToString() +
                FileFlags.GetHashCode().ToString() +
                MD5.GetHashCode().ToString() +
                Size.GetHashCode().ToString() +
                Version.GetHashCode().ToString();

            return IO.MD5FromString(hash);
        }

        public override string ToString() => (Folder ?? "NOFOLDER") + Path.DirectorySeparatorChar + (File ?? "NOFILE");
    }
}