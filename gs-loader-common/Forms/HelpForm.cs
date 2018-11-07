using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gs_loader_common.Forms
{
    public partial class HelpForm : Form
    {
        public HelpForm(string content)
        {
            InitializeComponent();
            rtbHelp.Text = content;
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            NotifyLoader.UpdateIcon(UpdateIconType.ShowBaloonInfo, "Linha de comando");
        }
    }
}
