namespace gs_loader_common.Programs
{
    public enum UpdateNeedType
    {
        /// <summary>
        /// Programa está com a versão atualizada
        /// </summary>
        Updated = 0,
        /// <summary>
        /// Programa está com versão mais antiga do que o repositório
        /// </summary>
        NeedUpdate = 1,
        /// <summary>
        /// Programa tem arquivos com diferenças em relação ao metadata esperado
        /// </summary>
        NotSyncedFiles = 2,
        /// <summary>
        /// Não há informação de repositório configurada no programa
        /// </summary>
        NoRepository = -1,
        /// <summary>
        /// Erro durante a consulta ao repositório
        /// </summary>
        RepositoryError = -2
    }
}