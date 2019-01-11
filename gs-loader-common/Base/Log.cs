using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace gs_loader_common.Base
{
    /// <summary>
    /// Classe de registro de Log da aplicação
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Flag que habilita o log de informações de Debug
        /// </summary>
        public static bool Debugging = true;

        const string LogPrefix = "GS-LOADER";
        static string LogPath;

        public static bool SetLogPath(string logPath)
        {
            if (IO.MakeFolder(logPath))
            {
                LogPath = logPath;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adiciona linha ao arquivo de log
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="tipoLog"></param>
        public static bool Add(string texto, string tipoLog = null)
        {
            if (string.IsNullOrEmpty(LogPath) || !Directory.Exists(LogPath))
                return false;

            try
            {
                string nomeArquivo = Path.Combine(LogPath, LogPrefix + "." + DateTime.Now.ToString("yyyyMMdd") + ".log");
                tipoLog = (tipoLog ?? "").Trim().PadRight(10).Substring(0, 10);
                bool NovoArquivo = !File.Exists(nomeArquivo);
                using (StreamWriter f = File.AppendText(nomeArquivo))
                {
                    if (NovoArquivo)
                    {
                        f.WriteLine("*** LOG  ***");
                        f.WriteLine("*** Padrão HHMMSS.fff");
                        f.WriteLine("*** Início do LOG ***");
                    }
                    var linhas = texto.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < linhas.Length; i++)
                        if (i == 0)
                            f.WriteLine(DateTime.Now.ToString("HHmmss.fff") + " " + tipoLog + " " + linhas[i]);
                        else f.WriteLine(new string(' ', 10 + 1 + 10 + 1) + linhas[i]);
                }
            }
            catch { }
            return true;
        }

        /// <summary>
        /// Arquiva logs dos dias anteriores
        /// </summary>
        public static void ArquivaAnteriores()
        {
            string[] logs = Directory.GetFiles(LogPath, "*.log");
            if (logs.Length == 0) return;
            string pastaArquivo = Path.Combine(LogPath, "arquivo");
            if (!IO.MakeFolder(pastaArquivo))
            {
                Dialog.Error("Não foi possível criar a pasta " + pastaArquivo + "\n\n" + IO.LastError);
                return;
            }

            foreach (string l in logs)
            {
                // Verifica se o arquivo encontrado é um log de hoje
                if (Path.GetFileName(l).StartsWith(LogPrefix + "." + DateTime.Now.ToString("yyyyMMdd")))
                    continue;

                if (IO.TryMove(l, Path.Combine(pastaArquivo, Path.GetFileName(l))))
                {
                    Debug("MOVE " + l + " para backup");
                    continue;
                }
                Debug("!MOVE " + l + " para backup: " + IO.LastError);
                if (IO.TryDelete(l))
                    Debug("DELETE " + l);
                else
                    Debug("!DELETE " + l + " : " + IO.LastError);

            }
        }

        /// <summary>
        /// Compacta logs arquivados em zips mensais
        /// </summary>
        public static void CompactaAnteriores()
        {
            string pastaArquivo = Path.Combine(LogPath, "arquivo");
            Debug("Log.CompactaAnteriores para " + pastaArquivo);
            string zip;
            Dictionary<string, ZipFile> zips = new Dictionary<string, ZipFile>();
            try
            {
                string[] logs = Directory.GetFiles(pastaArquivo, "*.log");
                if (logs.Length == 0) return;
                for (int i = 0; i < logs.Length; i++)
                {
                    // Verifica se o arquivo encontrado é um log de hoje
                    if (Path.GetFileName(logs[i]).StartsWith(DateTime.Now.ToString("yyyyMMdd")))
                        continue;

                    zip = Path.Combine(pastaArquivo, string.Format("Log_{0:yyyy_MM}.zip", IO.FileCreationTime(logs[i])));
                    ZipFile z;
                    if (zips.ContainsKey(zip))
                        z = zips[zip];
                    else
                    {
                        z = new ZipFile(zip);
                        zips.Add(zip, z);
                        Debug("Criando " + zip);
                    }

                    z.UpdateFile(logs[i], "");
                    Debug("+" + logs[i] + " em " + zip);
                }

                for (int i = 0; i < zips.Count; i++)
                {
                    zips.ElementAt(i).Value.Save();
                    zips.ElementAt(i).Value.Dispose();
                }
                for (int i = 0; i < logs.Length; i++)
                    if (!Path.GetFileName(logs[i]).StartsWith(DateTime.Now.ToString("yyyyMMdd")))
                        IO.TryDelete(logs[i]);
            }
            catch (Exception e)
            {
                Debug("!Log.CompactaAnteriores [" + e.Message + "]");
            }

            Debug("FIM de Log.CompactaAnterioes");
        }

        /// <summary>
        /// Cria log de debug, apenas se a propriedade Debug for true
        /// </summary>
        /// <param name="texto"></param>
        public static void Debug(string texto)
        {
            if (Debugging)
                Add(texto, "DEBUG");
        }

    }
}
