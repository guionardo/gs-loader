using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Telemetry
{
    public class TelemetryHost
    {
        /// <summary>
        /// Tipo do Host
        /// </summary>
        public TelemetryHostType HostType { get; set; }

        /// <summary>
        /// Destino do envio de telemetria (Pasta de arquivos de log, URL http[s])
        /// </summary>
        public string HostTarget { get; set; }


    }
}
