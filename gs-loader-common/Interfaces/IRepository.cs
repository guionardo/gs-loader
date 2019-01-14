using gs_loader_common.Telemetry;
using System.Collections.Generic;
using gs_loader_common.Programs;

namespace gs_loader_common.Interfaces
{
    public interface IRepository
    {
        /// <summary>
        /// Verifica se o repositório está OK
        /// </summary>
        /// <returns></returns>
        bool CheckRepository(out string message);

        /// <summary>
        /// Lista programas c
        /// </summary>
        /// <param name="programs"></param>
        /// <returns></returns>
        bool QueryPrograms(List<Program> programs);

        /// <summary>
        /// Envia dados de telemetria de execução, instalação e atualização e exceção
        /// </summary>
        /// <param name="telemetryMessage"></param>
        /// <returns></returns>
        bool SendTelemetry(TelemetryMessage telemetryMessage);

        /// <summary>
        /// Recebe todos os arquivos do repositório e copia para a pasta de cache
        /// </summary>
        /// <param name="programName"></param>
        /// <param name="cacheFolder"></param>
        /// <returns></returns>
        bool DownloadToCache(string programName, string cacheFolder);

        /// <summary>
        /// Envia programa para o repositório
        /// </summary>
        /// <param name="program"></param>
        /// <param name="setupFolder"></param>
        /// <returns></returns>
        bool PushProgram(Program program, string setupFolder);
        
    }
}
