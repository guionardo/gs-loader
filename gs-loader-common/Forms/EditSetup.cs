using gs_loader_common.Programs;
using gs_loader_common.Update;
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

namespace gs_loader_common.Forms
{
    public partial class EditSetup : Form
    {
        Program Program = null;

        private EditSetup(Program program)
        {
            InitializeComponent();
            Program = program;
            Text = "Configurações do programa: " + Program.ProgramName;
            txVersion.Text = Program.Version;
            txMainExecutable.Text = Path.GetFileName(Program.Main.FileName);
            txArguments.Text = Program.Arguments;
            chkJustOneInstance.Checked = Program.JustOneInstance;
            string req = "";
            foreach (var r in Program.Requirements)
                req += r.Trim() + " ";
            txRequirements.Text = req.Trim();

            chkUpdTypeBeforeRun.Checked = Program.UpdateType.HasFlag(UpdateType.BeforeRun);
            chkUpdTypeAfterRun.Checked = Program.UpdateType.HasFlag(UpdateType.AfterRun);
            chkUpdTypeOnceADay.Checked = Program.UpdateType.HasFlag(UpdateType.OnceADay);

            txRepositoryHost.Text = Program.RepositoryHost ?? "";
            txNotes.Text = Program.Notes ?? "";

            if (Program.Files.Count > 0)
                dgvFiles.RowCount = Program.Files.Count;
            else
                dgvFiles.Enabled = false;
        }

        public static bool Editar(Program program)
        {
            if (program == null)
                return false;

            using (var es = new EditSetup(program))
            {
                return es.ShowDialog() == DialogResult.OK;
            }
        }

        private void dgvFiles_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= Program.Files.Count)
                return;

            switch (e.ColumnIndex)
            {
                case 0:
                    e.Value = Program.Files[e.RowIndex].FileName;
                    break;
                case 1:
                    e.Value = Program.Files[e.RowIndex].Version.ToString();
                    break;
                case 2:
                    e.Value = Program.Files[e.RowIndex].Size;
                    break;
            }
        }
    }
}
