using gs_loader_common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Telemetry
{
    public class HTTPTelemetry : ITelemetry
    {
        public bool SendTelemetry(TelemetryMessage telemetryMessage)
        {
            //TODO: Implementar envio de telemetria HTTP
            return true;
        }
    }
}
