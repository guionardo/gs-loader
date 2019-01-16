using gs_loader_common.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Interfaces
{
    public interface ITelemetry
    {
        /// <summary>
        /// Envia dados de telemetria de execução, instalação e atualização e exceção
        /// </summary>
        /// <param name="telemetryMessage"></param>
        /// <returns></returns>
        bool SendTelemetry(TelemetryMessage telemetryMessage);
    }
}
