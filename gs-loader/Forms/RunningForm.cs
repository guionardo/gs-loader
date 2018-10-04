using gs_loader.Run;
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

namespace gs_loader.Forms
{
    public partial class RunningForm : Form
    {
        SetupData _setupData = null;
        string _basePath = "";
        public RunningForm(SetupData setupData, string basePath)
        {
            InitializeComponent();
            _setupData = setupData ?? throw new ArgumentNullException("setupData");
            if (string.IsNullOrEmpty(basePath))
                throw new ArgumentNullException("basePath");
            if (!Directory.Exists(basePath))
                throw new DirectoryNotFoundException(basePath);
            _basePath = basePath;
            notifyIcon1.Text = "GS-Loader: " + setupData.Executable.Description;

            if (DoRun.Run(_setupData, _basePath, out string message))
            {

            }
            Close();
        }

        private bool allowshowdisplay = false;
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
        }

        private void RunningForm_Load(object sender, EventArgs e)
        {
           
        }
    }
}
