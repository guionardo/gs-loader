using gs_loader_common.Base;
using System;
using System.IO;

namespace gs_loader_common.Setup
{
    /// <summary>
    /// Arquivo relacionado para cópia/atualização
    /// </summary>
    public class FileEntry : IComparable
    {
        private DateTime _fileTime = DateTime.MinValue;
        private string _md5 = "";
        private long _size = -1;

        /// <summary>
        /// Cria instância de FileEntry a partir de um arquivo real
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="basePath"></param>
        public FileEntry(string fileName, string basePath)
        {
            if (File.Exists(fileName))
            {
                FileName = Path.GetFileName(fileName);
                RealFilePath = Path.GetDirectoryName(fileName);
                BasePath = BasePath;
            }
        }

        /// <summary>
        /// Cria instância de FileEntry a partir de informações
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="basePath"></param>
        /// <param name="md5"></param>
        /// <param name="fileTime"></param>
        /// <param name="fileSize"></param>
        public FileEntry(string fileName, string basePath, string md5, DateTime fileTime, long fileSize)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException("Invalid fileName");
            if (!IO.IsMD5(md5))
                throw new ArgumentException("Invalid md5");
            if (fileSize <= 0)
                throw new ArgumentOutOfRangeException("fileSize", "must be 0 or greater");

            FileName = fileName;
            BasePath = basePath ?? throw new ArgumentNullException("basePath");
            _md5 = md5;
            _fileTime = fileTime;
            _size = fileSize;
        }

        public string BasePath { get; private set; }
        public string FileName { get; private set; }
        public DateTime FileTime
        {
            get
            {
                if (_fileTime == DateTime.MinValue)
                {
                    _fileTime = File.GetCreationTimeUtc(Path.Combine(RealFilePath, FileName));
                }
                return _fileTime;
            }
        }

        public long Size
        {
            get
            {
                if (_size < 0)
                {
                    FileInfo fi = new FileInfo(Path.Combine(RealFilePath, FileName));
                    _size = fi.Length;
                }
                return _size;
            }
        }
        public string MD5
        {
            get
            {
                if (string.IsNullOrEmpty(_md5))
                {
                    _md5 = IO.MD5(Path.Combine(RealFilePath, FileName));
                }
                return _md5;
            }
        }

        public string RealFilePath { get; private set; }
        /// <summary>
        /// Compara dois arquivos
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>-1 se this é mais antigo, 1 se other é mais antigo, e 0 se são iguais</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (!(obj is FileEntry))
                throw new ArgumentException("Uncomparable types");

            FileEntry other = (FileEntry)obj;

            if (Exists() && !other.Exists())
                return 1;
            else if (other.Exists() && !Exists())
                return -1;

            if (MD5 == other.MD5)
                return 0;

            return FileTime.CompareTo(other.FileTime);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FileEntry))
                return false;
            FileEntry other = (FileEntry)obj;
            if (other.MD5.Equals(this.MD5))
                return true;
            return false;
        }

        public bool Exists()
        {
            return File.Exists(Path.Combine(RealFilePath, FileName));
        }
        public override int GetHashCode() => (BasePath ?? "" + FileName ?? "").ToUpperInvariant().GetHashCode();
    }
}
