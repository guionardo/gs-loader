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
