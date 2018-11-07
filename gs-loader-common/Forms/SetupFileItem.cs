using gs_loader_common.Setup;
using System;

namespace gs_loader_common.Forms
{
    /// <summary>
    /// Item referente aos arquivos no SetupForm
    /// </summary>
    public class SetupFileItem:IComparable
    {
        public string FileName;

        public SetupFile SetupFile;
        public SetupFileState State;
        public bool Include;

        public int CompareTo(object obj)
        {
            var o = (SetupFileItem)obj;

            int r = SetupFile.Folder.CompareTo(o.SetupFile.Folder);
            if (r == 0)
                r = FileName.CompareTo(o.FileName);
            return r;
        }
    }

    public enum SetupFileState
    {
        /// <summary>
        /// Arquivo consta na pasta local e na lista de setup
        /// </summary>
        Synced = 0,
        /// <summary>
        /// Arquivo consta apenas na pasta local
        /// </summary>
        OnFolder = 1,
        /// <summary>
        /// Arquivo consta apenas na pasta local e sua extensão não está incluída
        /// </summary>
        OnFolderUnselected = 2,
        /// <summary>
        /// Arquivo consta apenas na lista de setup
        /// </summary>
        OnSetup = 3
    }
}
