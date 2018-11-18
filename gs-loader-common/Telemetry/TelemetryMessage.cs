using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Telemetry
{
    /// <summary>
    /// Mensagem para telemetria
    /// </summary>
    public class TelemetryMessage
    {
        public string SetupName { get; set; }
        public string OldVersion { get; set; }
        public string NewVersion { get; set; }
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// Nome da estação de trabalho
        /// </summary>
        public string StationName { get; set; }
        //TODO: Definir os campos para a mensagem de telemetria
    }
}
