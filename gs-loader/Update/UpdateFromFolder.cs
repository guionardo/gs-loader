using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gs_loader.Setup;

namespace gs_loader.Update
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
