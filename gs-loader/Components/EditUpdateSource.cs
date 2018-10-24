using System;
using System.Windows.Forms;
using gs_loader.Update;

namespace gs_loader.Components
{
    public partial class EditUpdateSource : UserControl
    {
        public EditUpdateSource()
        {
            InitializeComponent();
            cmbUpdateSource.SelectedIndex = 0;
            cmbUpdateType.SelectedIndex = 0;
        }

        public UpdateSource UpdateSource
        {
            get
            {
                UpdateSource update = new UpdateSource
                {
                    Type = (UpdateSourceType)cmbUpdateSource.SelectedIndex,
                    Address = txAddress.Text,
                    UserName = txUser.Text,
                    Password = txPass.Text
                };
                return update;
            }
            set
            {
                if (value == null)
                    value = new UpdateSource();

                if ((int)value.Type < 0)
                    value.Type = UpdateSourceType.Folder;
                cmbUpdateSource.SelectedIndex = (int)value.Type;
                txAddress.Text = value.Address;
                txUser.Text = value.UserName;
                txPass.Text = value.Password;
            }
        }
        public UpdateType UpdateType
        {
            get
            {
                UpdateType update = UpdateType.None;

                if (chkOnceADay.Checked)
                    update |= UpdateType.OnceADay;
                switch (cmbUpdateType.SelectedIndex)
                {
                    case 1:
                        update |= UpdateType.BeforeRun;
                        break;
                    case 2:
                        update |= UpdateType.AfterRun;
                        break;
                }

                return update;
            }
            set
            {
                chkOnceADay.Checked = value.HasFlag(UpdateType.OnceADay);
                if (value.HasFlag(UpdateType.BeforeRun))
                    cmbUpdateType.SelectedIndex = 1;
                else if (value.HasFlag(UpdateType.AfterRun))
                    cmbUpdateType.SelectedIndex = 2;
                else
                    cmbUpdateType.SelectedIndex = 0;
            }
        }

        private void cmbUpdateSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbUpdateType.SelectedIndex)
            {
                case 0:
                    lblAdress.Text = "Pasta";
                    break;
                case 1:
                    lblAdress.Text = "FTP";
                    break;
                case 2:
                    lblAdress.Text = "HTTP";
                    break;
            }
        }
    }
}
