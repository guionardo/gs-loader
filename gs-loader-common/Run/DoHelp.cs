using gs_loader_common.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gs_loader_common.Run
{
    public static class DoHelp
    {
        public static Form ShowHelp(string content)
        {
            return new Forms.HelpForm(Strings.Get(StringName.OptionHelpMessage)
                .Replace("%EXE%", Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location))
                .Replace("%OPT%", content));

            //DONE: Apresentar o Help

        }
    }
}
