using gs_loader_common.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gs_loader_common.Hosts
{
    public class Host
    {
        public HostType HostType { get; private set; }
        public string HostName { get; private set; }

        private Host(HostType hostType, string hostName)
        {
            this.HostType = hostType;
            this.HostName = hostName;
        }

        public static Host FromName(string hostName)
        {
            // http://repositorio.info/teste
            // https://repositorioseguro.com/outro
            if (Regex.IsMatch(hostName, @"(http|https):\/\/(.*)"))
                return new Host(HostType.HTTP, hostName);

            // file://C:\REPOSITORIO\SUBPASTA
            if (Regex.IsMatch(hostName, @"(file):\/\/(.*)"))
                return new Host(HostType.LocalFolder, hostName.Substring(7));

            // C:\REPOSITORIO\SUBPASTA
            if (Regex.IsMatch(hostName, @"(?:[a-zA-Z]\:)\\(?:[\w]+\\)*\w([\w.])"))
                return new Host(HostType.LocalFolder, hostName);

            // \\COMPUTADOR\COMPARTILHAMENTO
            if (Regex.IsMatch(hostName, @"(\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])"))
                return new Host(HostType.SharedFolder, hostName);

            if (IO.IsValidFilename(hostName))
                return new Host(HostType.LocalFolder, hostName);

            return new Host(HostType.None, "");
        }

        public override string ToString() => HostType + " " + HostName;
        
    }
}
