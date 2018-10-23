using gs_loader.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gs_loader.Components;

namespace gs_loader.Forms
{
    public partial class SetupForm : Form
    {
        public SetupForm(SetupData setupData)
        {
            InitializeComponent();
            includeExts.DefaultValues = SetupData.DefaultExtensions;
            ignoreExts.DefaultValues = SetupData.DefaultIgnoredExtensions;

            includeExts.Value = setupData.IncludeExtensions;
            ignoreExts.Value = setupData.IgnoredExtensions;
        }
    }
}
