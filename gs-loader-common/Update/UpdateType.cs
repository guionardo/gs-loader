using System;

namespace gs_loader_common.Update
{
    /// <summary>
    /// Tipo da atualização, referente a quando ocorrerá
    /// </summary>
    [Flags]
    public enum UpdateType
    {
        None = 0,
        BeforeRun = 1,
        AfterRun = 2,
        OnceADay = 4
    }
}
