using gs_loader_common.Base;
using gs_loader_common.Setup;
using System;

namespace gs_loader_common.Forms
{

    /// <summary>
    /// Item referente aos arquivos no SetupForm
    /// </summary>
    public class SetupFileItem : IComparable
    {
        public bool Executable
        {
            get { return SetupFile.Executable; }
            set
            {
                if (value && !IO.IsExecutable(FileName)) return;
                SetupFile.Executable = value;
            }
        }
        public string FileName;

        public bool Include;
        public SetupFile SetupFile;
        public SetupFileState State;
        public int CompareTo(object obj)
        {
            var o = (SetupFileItem)obj;

            int r = SetupFile.Folder.CompareTo(o.SetupFile.Folder);
            if (r == 0)
                r = FileName.CompareTo(o.FileName);
            return r;
        }
    }
}
