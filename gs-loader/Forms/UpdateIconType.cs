namespace gs_loader.Forms
{
    public enum UpdateIconType
    {
        /// <summary>
        /// Atualiza o text do icone
        /// </summary>
        Text,
        /// <summary>
        /// Ativa/desativa o icone
        /// </summary>
        Visible,
        /// <summary>
        /// Registra a informação do processo para visualização via ContextMenu
        /// </summary>
        ProcessInfo,
        /// <summary>
        /// Mostra informação com o balão 
        /// </summary>
        ShowBaloonInfo,
        /// <summary>
        /// Mostra erro com o balão
        /// </summary>
        ShowBalloonError,
        /// <summary>
        /// Define descrição do processo
        /// </summary>
        ProcessDescription,
        /// <summary>
        /// Fechar o balão
        /// </summary>
        CloseBaloon,
        /// <summary>
        /// Define o ícone do traybar
        /// </summary>
        SetIcon,
        /// <summary>
        /// Restaura o ícone do traybar
        /// </summary>
        RestoreIcon
    }
}
