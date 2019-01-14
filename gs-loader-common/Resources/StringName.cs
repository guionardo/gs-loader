namespace gs_loader_common.Resources
{
    public enum StringName
    {
        OptionHelp,
        OptionRun,
        OptionInstall,
        OptionUpdate,
        OptionVerify,
        OptionStats,
        /// <summary>
        /// Parâmetro inválido: %MESSAGE%\nExecute com o parâmetro --help para mais informações.
        /// </summary>
        OptionErrorParameter,
        OptionErrorRepository,
        OptionErrorRepositoryNotFound,
        OptionSetup,
        OptionHelpMessage,
        /// <summary>
        /// Arquivo inexistente: %FILE%
        /// </summary>
        FileNotFound,
        FileSizeDiff,
        FileMD5Diff,
        FileIdentical,
        /// <summary>
        /// Arquivo não foi criado: %FILE% (%ERROR%)
        /// </summary>
        FileExecutableNotFound,
        ExecutableFile,
        ExecutableNotInformed,
        /// <summary>
        /// Pasta inexistente: %DIR%
        /// </summary>
        DirectoryNotFound,
        DirectoryNotCreated,
        OptionErrorProgram,
        OptionHost,
        OptionProgram,
        /// <summary>
        /// Repositório não identificado: %REPO%
        /// </summary>
        OptionErrorRepositoryType,
        FileReadMe,
        FileNotCreated
    }
}
