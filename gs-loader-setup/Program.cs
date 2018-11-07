using gs_loader_common.Base;
using gs_loader_common.Setup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gs_loader_setup
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Output.NoGUI = false;   // Assegura que as saída serão via form/message

            SetupData setupData = null;
            if (args.Length > 0)
            {
                // Se houver um argumento, pelo menos é um arquivo ou pasta onde o setup está configurado                
                    if (!SetupData.Read(args[0], out setupData, out string msg))
                    {                        
                        Dialog.Error("Arquivo de setup [" + args[0] + " não foi aberto.\n" + msg);
                        return;
                    }
                
            }

            Application.Run(new MainForm(setupData));
        }
    }
}
