using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Interfaces
{
    public interface IPlugin
    {
        /// <summary>
        /// Obter o nome do cliente
        /// </summary>
        /// <returns></returns>
        string GetClientName();
        /// <summary>
        /// Obter o nome da estação de trabalho
        /// </summary>
        /// <returns></returns>
        string GetStationName();
    }
}
