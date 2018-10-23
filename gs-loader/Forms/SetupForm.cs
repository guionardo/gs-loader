﻿using gs_loader.Setup;
using gs_loader.Base;
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
        List<SetupFileItem> setupFiles = new List<SetupFileItem>();

        public SetupForm(SetupData setupData)
        {
            InitializeComponent();
            includeExts.DefaultValues = SetupData.DefaultExtensions;
            ignoreExts.DefaultValues = SetupData.DefaultIgnoredExtensions;

            includeExts.Value = setupData.IncludeExtensions;
            ignoreExts.Value = setupData.IgnoredExtensions;

            lblOriginFolder.Text = setupData.SetupFile;

            ParseFiles(Path.GetDirectoryName(setupData.SetupFile), setupData.Files);
        }

        private void ParseFiles(string setupPath, List<SetupFile> files)
        {
            setupFiles.Clear();

            var f = Directory.GetFiles(setupPath, "*.*", SearchOption.AllDirectories).ToList();

            // Remove arquivos com extensões ignoradas
            int i = 0;
            while (i < f.Count())
            {
                string ext = Path.GetExtension(f[i]);
                if (Extensions.Contains(ignoreExts.Value, ext))
                    f.RemoveAt(i);
                else i++;
            }

            // Adiciona arquivos existentes na pasta
            for (i = 0; i < f.Count; i++)
            {
                SetupFileState fileState = SetupFileState.OnFolderUnselected;
                // Verifica se a extensão não faz parte da lista de extensões requeridas
                if (Extensions.Contains(includeExts.Value, Path.GetExtension(f[i])))
                    fileState = SetupFileState.OnFolder;

                setupFiles.Add(new SetupFileItem
                {
                    FileName = f[i],
                    State = fileState,
                    SetupFile = new SetupFile(f[i], setupPath),
                    Include = fileState == SetupFileState.OnFolder
                });
            }

            // Adiciona arquivos do setup
            for (i = 0; i < files.Count; i++)
            {
                int k = -1;
                for (int j = 0; j < setupFiles.Count; i++)
                {
                    string setupFileName = setupFiles[i].FileName.Substring(setupPath.Length);
                    if (setupFileName.Equals(Path.Combine(files[i].Folder, files[i].File), StringComparison.InvariantCultureIgnoreCase))
                    {
                        k = j;
                        break;
                    }
                }
                if (k == -1)
                {
                    // Não existe na lista atual
                    setupFiles.Add(new SetupFileItem
                    {
                        FileName = Path.Combine(setupPath, files[i].Folder, files[i].File),
                        SetupFile = new SetupFile(Path.Combine(setupPath, files[i].Folder, files[i].File)),
                        State = SetupFileState.OnSetup
                    });
                }
                else
                {
                    // Atualiza a lista
                    setupFiles[k].State = SetupFileState.Synced;
                    setupFiles[k].Include = true;
                }
            }

            // Ordena a lista
            setupFiles.Sort((x, y) => x.FileName.CompareTo(y.FileName));

            dgvFiles.RowCount = setupFiles.Count;
        }

        private void dgvFiles_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= setupFiles.Count)
                return;
            switch (e.ColumnIndex)
            {
                case 0: // Arquivo local
                    e.Value = setupFiles[e.RowIndex].FileName;
                    break;
                case 1: // Pasta
                    if (setupFiles[e.RowIndex].SetupFile == null)
                        e.Value = "";
                    else
                        e.Value = setupFiles[e.RowIndex].SetupFile.Folder;
                    break;
                case 2: // Estado
                    e.Value = setupFiles[e.RowIndex].State;
                    break;
                case 3: // Tamanho
                    e.Value = setupFiles[e.RowIndex].SetupFile.Size;
                    break;
                case 4: // MD-5
                    e.Value = setupFiles[e.RowIndex].SetupFile.MD5;
                    break;
                case 5: // Incluir
                    e.Value = setupFiles[e.RowIndex].Include;
                    break;
            }
        }

        private void dgvFiles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= setupFiles.Count || e.ColumnIndex != 5)
                return;
            setupFiles[e.RowIndex].Include = !setupFiles[e.RowIndex].Include;
        }
    }
}
