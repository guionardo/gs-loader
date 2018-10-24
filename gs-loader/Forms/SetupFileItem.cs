using gs_loader.Setup;

namespace gs_loader.Forms
{
    /// <summary>
    /// Item referente aos arquivos no SetupForm
    /// </summary>
    public class SetupFileItem
    {
        public string FileName;

        public SetupFile SetupFile;
        public SetupFileState State;
        public bool Include;
        
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
