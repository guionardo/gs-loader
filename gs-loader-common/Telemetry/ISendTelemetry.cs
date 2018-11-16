using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Telemetry
{
    public interface ISendTelemetry
    {
        bool Send(TelemetryHost telemetryHost, TelemetryMessage telemetryMessage);
    }
}
