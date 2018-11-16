using gs_loader_common.Base;
using System;
using System.Collections.Generic;

namespace gs_loader_setup
{
    /// <summary>
    /// Classe para controle das configurações 
    /// </summary>
    public static class Config
    {
        public static readonly List<string> LastFiles = new List<string>();

        static Config()
        {
            LoadLastFiles();
        }

        static bool Reload()
        {
            try
            {
                Properties.Settings.Default.Reload();
                return true;
            }
            catch (Exception e)
            {
                Dialog.Error("Ocorreu uma exceção ao carregar as configurações da aplicação.\nEstas informações serão reiniciadas.\n\n" + e.Message);
            }
            try
            {
                Properties.Settings.Default.Reset();
            }
            catch (Exception e)
            {
                Dialog.Error("Ocorreu uma exceção ao reiniciar as configurações da aplicação.\n\n" + e.Message);
                return false;
            }
            try
            {
                Properties.Settings.Default.Save();
                return true;
            }
            catch (Exception e)
            {
                Dialog.Error("Ocorreu uma exceção ao gravar as configurações da aplicação.\n\n" + e.Message);
                return false;
            }
        }

        public static bool AddLastFile(string fileName)
        {
            if (!Reload())
                return false;
            try
            {
                if (Properties.Settings.Default.LastFiles == null)
                    Properties.Settings.Default.LastFiles = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.LastFiles.Remove(fileName);
                Properties.Settings.Default.LastFiles.Insert(0, fileName);
                while (Properties.Settings.Default.LastFiles.Count > 5)
                    Properties.Settings.Default.LastFiles.RemoveAt(5);
                Properties.Settings.Default.Save();
                LoadLastFiles();
                return true;
            }
            catch (Exception e)
            {
                Dialog.Error("Não foi possível gravar informações de configuração da aplicação!\n\n" + e.Message);
            }
            return false;
        }

        static bool LoadLastFiles()
        {
            LastFiles.Clear();
            bool erroLoad = false;
            try
            {
                Properties.Settings.Default.Reload();
            }
            catch (Exception e)
            {
                Dialog.Error("Ocorreu uma exceção ao carregar as configurações da aplicação.\nEstas informações serão reiniciadas.\n\n" + e.Message);
                erroLoad = true;
            }
            if (erroLoad)
                try
                {
                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.Save();
                    erroLoad = false;
                }
                catch (Exception e)
                {
                    Dialog.Error("Não foi possível reiniciar as informações de configuração da aplicação!\n\n" + e.Message);
                }

            if (erroLoad)
                return false;
            if (Properties.Settings.Default.LastFiles != null)
                foreach (var f in Properties.Settings.Default.LastFiles)
                    LastFiles.Add(f);

            return true;
        }
    }
}
