namespace gs_loader_common.Hosts
{
    /// <summary>
    /// Tipo para um host
    /// </summary>
    public enum HostType
    {
        /// <summary>
        /// Nenhum
        /// </summary>
        None = 0,
        /// <summary>
        /// Pasta de sistema
        /// </summary>
        LocalFolder = 1,
        /// <summary>
        /// Pasta de rede compartilhada
        /// </summary>
        SharedFolder = 2,
        /// <summary>
        /// HTTP
        /// </summary>
        HTTP = 3
    }
}
