namespace gs_loader_common.Setup
{
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
