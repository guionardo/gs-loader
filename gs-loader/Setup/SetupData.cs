﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace gs_loader.Setup
{
    /// <summary>
    /// Dados de setup do programa a ser executado/atualizado
    /// </summary>
    public class SetupData
    {
        public const string DefaultFileName = "gs-loader.json";
        public static readonly string[] DefaultExtensions = new string[] { "*.exe", "*.dll" };
        public SetupFile Executable { get; set; }
        public List<SetupFile> Files { get; set; } = new List<SetupFile>();

        public static bool Create(string executableFile, out SetupData setupData, out string message, string[] files = null)
        {
            if (files == null)
                files = DefaultExtensions;

            setupData = null;
            if (Directory.Exists(executableFile))
            {
                var exes = Directory.GetFiles(executableFile, "*.exe");
                switch (exes.Length)
                {
                    case 0:
                        message = "Não foram encontrados executáveis em " + executableFile;
                        return false;
                    case 1:
                        executableFile = exes[0];
                        break;
                    default:
                        bool found = false;
                        foreach (var f in exes)
                            if (Path.GetFileNameWithoutExtension(f).Equals(Path.GetFileName(executableFile), StringComparison.InvariantCultureIgnoreCase))
                            {
                                executableFile = f;
                                found = true;
                                break;
                            }
                        if (found)
                            break;
                        message = "Foram encontrados mais de um executável em " + executableFile;
                        return false;
                }
            }
            if (!File.Exists(executableFile))
            {
                message = "Executável inexistente em " + executableFile;
                return false;
            }
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(executableFile);
            string path = Path.GetDirectoryName(executableFile);
            setupData = new SetupData
            {
                Executable = new SetupFile(executableFile)
            };

            foreach (var f in files)
            {
                var f0 = Directory.GetFiles(path, f, SearchOption.AllDirectories);
                foreach (var f1 in f0)
                    if (!f1.Equals(executableFile, StringComparison.InvariantCultureIgnoreCase))
                        setupData.Files.Add(new SetupFile(f1, path));
            }


            message = "OK";
            return true;
        }

        /// <summary>
        /// Lê o setup a partir do arquivo
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="setupData"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Read(string fileName, out SetupData setupData, out string message)
        {
            setupData = null;
            message = "";
            fileName = ParseFileName(fileName);
            if (string.IsNullOrEmpty(fileName))
            {
                message = "Argumento inválido ou vazio";
                return false;
            }

            if (!File.Exists(fileName))
            {
                message = "Arquivo de setup inexistente em " + fileName;
                return false;
            }

            try
            {
                string json = File.ReadAllText(fileName);
                setupData = JsonConvert.DeserializeObject<SetupData>(json);
                message = "OK";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Gravar setup no arquivo
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="setupData"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Write(string fileName, SetupData setupData, out string message)
        {
            fileName = ParseFileName(fileName);
            if (string.IsNullOrEmpty(fileName))
            {
                message = "Destino do setup inválido ou vazio";
                return false;
            }
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                message = "Caminho do arquivo de setup é inexistente (" + Path.GetDirectoryName(fileName) + ")";
                return false;
            }
            try
            {
                string json = JsonConvert.SerializeObject(setupData, Formatting.Indented);
                File.WriteAllText(fileName, json);
                message = "OK";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Trata o nome do arquivo caso for informado apenas a pasta
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string ParseFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "";
            fileName = Path.GetFullPath(fileName);
            if (Directory.Exists(fileName))
                fileName = Path.Combine(fileName, DefaultFileName);
            return fileName;
        }
    }
}
