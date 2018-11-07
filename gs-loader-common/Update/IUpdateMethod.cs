using gs_loader_common.Setup;

namespace gs_loader_common.Update
{
    public interface IUpdateMethod
    {
        /// <summary>
        /// Faz o download de todos os arquivos para uma pasta temporária
        /// </summary>
        /// <param name="updateSource"></param>
        /// <param name="cacheFolder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool DownloadToCache(UpdateSource updateSource, out string cacheFolder, out string message);

        /// <summary>
        /// Obtém o setupData com os dados da última versão 
        /// </summary>
        /// <param name="updateSource"></param>
        /// <param name="setupData"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool GetSetupData(UpdateSource updateSource, out SetupData setupData, out string message);
    }
}
