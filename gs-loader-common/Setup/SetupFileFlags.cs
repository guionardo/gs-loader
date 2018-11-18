using System;

namespace gs_loader_common.Setup
{
    [Flags]
    public enum SetupFileFlags
    {
        None = 0,
        Include = 1,
        MainExecutable = 2,
        Icon = 4,
        CreateDesktopShortCut = 8
    }
}