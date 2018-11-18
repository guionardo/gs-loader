using gs_loader_common.Setup;
using System;
using System.Collections.Generic;
using System.IO;

namespace gs_loader_common.Base
{
    public static class Extensions
    {
        public static bool Contains(this string[] array, string value)
        {
            foreach (var item in array)
                if (item.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }

        /// <summary>
        /// Retorna índice de um setup file em uma lista
        /// </summary>
        /// <param name="setupFiles"></param>
        /// <param name="setupFile"></param>
        /// <returns></returns>
        public static int IndexOfFile(this List<SetupFile> setupFiles, SetupFile setupFile)
        {
            for (int i = 0; i < setupFiles.Count; i++)
                if (setupFiles[i].CompareTo(setupFile) == 0)
                    return i;
            return -1;
        }
        /// <summary>
        /// Ordena uma lista de setup file, de acordo com a pasta/arquivo
        /// </summary>
        /// <param name="setupFiles"></param>
        public static void SortFiles(this List<SetupFile> setupFiles)
        {
            setupFiles.Sort((x, y) => x.CompareTo(y));
        }

        /// <summary>
        /// Adiciona os arquivos de uma pasta a uma lista de SetupFile já populada
        /// </summary>
        /// <param name="setupFiles"></param>
        /// <param name="folder"></param>
        /// <param name="ignoredExts"></param>
        /// <param name="includedExts"></param>
        /// <returns></returns>
        public static int AddFilesFromFolder(this List<SetupFile> setupFiles, string folder, string[] ignoredExts = null, string[] includedExts = null)
        {
            int added = 0;
            if (ignoredExts == null) ignoredExts = SetupData.DefaultIgnoredExtensions;
            if (includedExts == null) includedExts = SetupData.DefaultExtensions;

            var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                SetupFile setupFile = new SetupFile(f, folder);
                var index = setupFiles.IndexOfFile(setupFile);
                if (Contains(ignoredExts, Path.GetExtension(f)))
                {
                    // Extensão ignorada
                    setupFile.State = SetupFileState.OnFolderUnselected;
                    setupFile.FileFlags &= ~SetupFileFlags.Include;
                }
                else
                if (Contains(includedExts, Path.GetExtension(f)))
                {
                    // Extensão incluída
                    setupFile.State = index == -1 ? SetupFileState.OnFolder : SetupFileState.Synced;
                    setupFile.FileFlags |= SetupFileFlags.Include;
                }
                else
                {
                    // Arquivo existente na pasta
                    setupFile.State = index == -1 ? SetupFileState.OnFolder : SetupFileState.Synced;
                    setupFile.FileFlags &= ~SetupFileFlags.Include;
                }
                if (index == -1)
                {
                    setupFiles.Add(setupFile);
                    added++;
                }
                else
                    setupFiles[index].Assign(setupFile);
            }
            setupFiles.SortFiles();
            return added;
        }

        public static string[] Copy(this string[] origin)
        {
            if (origin == null || origin.Length == 0)
                return new string[0];

            string[] destiny = new string[origin.Length];
            for (int i = 0; i < origin.Length; i++)
                destiny[i] = origin[i];
            return destiny;
        }

        /// <summary>
        /// Assegura que não existam itens duplicados
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string[] GetUnique(this string[] values)
        {
            List<string> listaV = new List<string>();
            foreach (var lin in values)
            {
                string l = lin;
                // Remove * e . do início da extensão
                while (l.StartsWith(".") || l.StartsWith("*"))
                    l = l.Substring(1);

                l = "." + l;

                bool found = false;
                foreach (var i in listaV)
                    if (l.Equals(i, StringComparison.InvariantCultureIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                if (!found)
                    listaV.Add(l);
            }
            return listaV.ToArray();
        }

        public static string MimeType(string extension)
        {
            return System.Web.MimeMapping.GetMimeMapping("test" + extension);
        }
        
        public static void Add(this SetupFileFlags flags, SetupFileFlags value)
        {
            flags |= value;
        }

        public static void Remove(this SetupFileFlags flags, SetupFileFlags value)
        {
            flags &= ~value;
        }

        public static void Invert(this SetupFileFlags flags, SetupFileFlags value)
        {
            if (flags.HasFlag(value))
                flags.Remove(value);
            else
                flags.Add(value);
        }
    }
}
