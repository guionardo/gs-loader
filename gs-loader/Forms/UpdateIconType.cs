using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader.Forms
{
    public enum UpdateIconType
    {
        /// <summary>
        /// Atualiza o text do icone
        /// </summary>
        Text,
        /// <summary>
        /// Ativa/desativa o icone
        /// </summary>
        Visible,
        /// <summary>
        /// Registra a informação do processo para visualização via ContextMenu
        /// </summary>
        ProcessInfo
    }
}
