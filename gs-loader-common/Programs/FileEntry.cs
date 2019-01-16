using gs_loader_common.Base;
using gs_loader_common.Resources;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace gs_loader_common.Programs
{
    /// <summary>
    /// Arquivo relacionado para cópia/atualização
    /// </summary>
    public class FileEntry : IComparable, ICloneable
    {
        private string _filedescription;
        private DateTime _fileTime = DateTime.MinValue;
        private string _filetype;
        private string _md5 = "";
        private long _size = -1;
        private Base.Version _version = null;
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
                if (string.IsNullOrEmpty(basePath))
                    basePath = Path.GetDirectoryName(fileName);

                BasePath = basePath;

                // Forçar carregamento das informações do arquivo
                var v = this.Version;
                var s = this.Size;
                var m = this.MD5;
                var t = this.FileType;
                var d = this.Description;


                if (RealFilePath.Length >= BasePath.Length)
                    Folder = RealFilePath.Substring(BasePath.Length);
                else
                    Folder = "";
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
        public FileEntry(string fileName, string basePath, string md5, DateTime fileTime, long fileSize, object version)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException("Invalid fileName");
            if (!IO.IsMD5(md5))
                throw new ArgumentException("Invalid md5");
            if (fileSize <= 0)
                throw new ArgumentOutOfRangeException("fileSize", "must be 0 or greater");
            if (version != null)
                if (version is string)
                    _version = new Base.Version((string)version);
                else if (version is Base.Version)
                    _version = (Base.Version)((Base.Version)version).Clone();
                else if (version is System.Version)
                    _version = new Base.Version(((System.Version)version).ToString());

            FileName = fileName;
            BasePath = basePath ?? throw new ArgumentNullException("basePath");
            _md5 = md5;
            _fileTime = fileTime;
            _size = fileSize;
        }

        private FileEntry()
        {

        }

        /// <summary>
        /// Pasta raiz do programa
        /// </summary>
        public string BasePath { get; private set; }

        /// <summary>
        /// Descrição do arquivo
        /// </summary>
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_filedescription))
                {
                    var rfn = RealFileName;
                    if (!string.IsNullOrEmpty(rfn))
                    {
                        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(rfn);
                        if (!string.IsNullOrEmpty(fvi.FileDescription))
                            _filedescription = fvi.FileDescription;
                        else
                            _filedescription = FileType;
                    }
                }
                return _filedescription;
            }
        }

        /// <summary>
        /// Nome do programa
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Data/hora de criação do arquivo (UTC)
        /// </summary>
        public DateTime FileTime
        {
            get
            {
                if (_fileTime == DateTime.MinValue)
                {
                    var rfn = RealFileName;
                    if (!string.IsNullOrEmpty(rfn))
                    {
                        _fileTime = File.GetCreationTimeUtc(rfn);
                    }
                }
                return _fileTime;
            }
        }

        /// <summary>
        /// Tipo MIME do arquivo
        /// </summary>
        public string FileType
        {
            get
            {
                if (string.IsNullOrEmpty(_filetype))
                {
                    _filetype = System.Web.MimeMapping.GetMimeMapping(FileName);
                }
                return _filetype;
            }
        }

        /// <summary>
        /// Subpasta do programa (vazio para pasta raiz)
        /// </summary>
        public string Folder { get; private set; }

        /// <summary>
        /// MD5 do arquivo
        /// </summary>
        public string MD5
        {
            get
            {
                if (string.IsNullOrEmpty(_md5))
                {
                    var rfn = RealFileName;
                    if (!string.IsNullOrEmpty(rfn))
                    {
                        _md5 = IO.MD5(rfn);
                    }
                }
                return _md5;
            }
        }

        public string RealFileName => File.Exists(Path.Combine(RealFilePath, FileName)) ? Path.Combine(RealFilePath, FileName) : "";
        [JsonIgnore]
        public string RealFilePath { get; private set; }

        public long Size
        {
            get
            {
                if (_size < 0)
                {
                    var rfn = RealFileName;
                    if (!string.IsNullOrEmpty(rfn))
                    {
                        FileInfo fi = new FileInfo(rfn);
                        _size = fi.Length;
                    }
                }
                return _size;
            }
        }

        public Base.Version Version
        {
            get
            {
                if (_version == null)
                {
                    var rfn = RealFileName;
                    if (!string.IsNullOrEmpty(rfn))
                    {
                        _version = Base.Version.FromFile(rfn);
                    }
                }
                return _version;
            }
        }

        public object Clone() => new FileEntry()
        {
            BasePath = this.BasePath,
            FileName = this.FileName,
            RealFilePath = this.RealFilePath,
            Folder = this.Folder,
            _fileTime = this.FileTime,
            _filetype = this.FileType,
            _md5 = this.MD5,
            _size = this.Size,
            _version = this.Version
        };

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

            var vcomp = Version.CompareTo(other.Version);
            if (vcomp != 0)
                return vcomp;

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

        public bool Exists() => File.Exists(Path.Combine(RealFilePath, FileName));

        public override int GetHashCode() => (BasePath ?? "" + "\\" + FileName ?? "").ToUpperInvariant().GetHashCode();

        public override string ToString() => Folder + (string.IsNullOrEmpty(Folder) ? "" : "\\") + FileName;
        /// <summary>
        /// Atualiza campos a partir de um arquivo existente
        /// </summary>
        /// <returns></returns>
        public bool UpdateInfos(string fileName)
        {
            if (!File.Exists(fileName))
                return false;

            if (_fileTime == DateTime.MinValue)
                _fileTime = File.GetCreationTimeUtc(fileName);

            if (string.IsNullOrEmpty(_md5))
                _md5 = IO.MD5(fileName);

            if (_size < 0)
            {
                FileInfo fi = new FileInfo(fileName);
                _size = fi.Length;
            }

            return true;
        }

        public bool Valid(string basePath, out string message)
        {
            string realFile = Path.Combine(basePath, Folder, FileName);
            if (!File.Exists(realFile))
            {
                message = Strings.Get(StringName.FileNotFound, "FILE", realFile);
                return false;
            }

            FileInfo fi = new FileInfo(realFile);
            if (fi.Length != Size)
            {
                message = Strings.Get(StringName.FileSizeDiff, "FILE", realFile, "EXPECTED", Size.ToString(), "REAL", fi.Length.ToString());
                return false;
            }

            string md5 = IO.MD5(realFile);
            if (md5 != MD5)
            {
                message = Strings.Get(StringName.FileMD5Diff, "FILE", realFile, "EXPECTED", MD5, "REAL", md5);
                return false;
            }
            message = Strings.Get(StringName.FileIdentical, "FILE", realFile);
            return true;
        }
    }
}
