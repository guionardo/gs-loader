using System;
using gs_loader_common.Setup;

namespace gs_loader_common.Update
{
    public class UpdateFromFolder : IUpdateMethod
    {
        public bool DownloadToCache(UpdateSource updateSource, out string tempFolder, out string message)
        {
            throw new NotImplementedException();
        }

        public bool GetSetupData(UpdateSource updateSource, out SetupData setupData, out string message)
        {
            throw new NotImplementedException();
        }
    }
}
