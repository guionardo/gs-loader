using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Base
{
    /// <summary>
    /// Classe para controle da pasta de setup
    /// </summary>
    public class SetupFolder
    {
        /// <summary>
        /// Inicializa pasta de setup
        /// </summary>
        /// <param name="folder">pasta da aplicação</param>
        public SetupFolder(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                throw new ArgumentNullException(folder, "Pasta da aplicação deve ser informada");

            if (!Directory.Exists(folder))
                throw new DirectoryNotFoundException("Pasta da aplicação inexistente (" + folder + ")");

            AppPath = Path.GetFullPath(folder);
            SetupPath = Path.Combine(AppPath, ".gsloader");

            if (!IO.MakeFolder(SetupPath))
                throw new DirectoryNotFoundException("Erro ao criar pasta de setup:" + IO.LastError, IO.LastException);

            var fa = File.GetAttributes(SetupPath);
            if (!fa.HasFlag(FileAttributes.Hidden))
                File.SetAttributes(SetupPath, fa | FileAttributes.Hidden);
        }

        /// <summary>
        /// Local da aplicação
        /// </summary>
        public readonly string AppPath;

        /// <summary>
        /// Local da pasta de setup (.gsloader)
        /// </summary>
        public readonly string SetupPath;
    }
}
