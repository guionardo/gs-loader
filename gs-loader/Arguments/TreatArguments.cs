﻿using gs_loader.Operations;
using gs_loader_common.Base;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace gs_loader.Arguments
{
    public class TreatArguments
    {

        static OptionSet Options = new OptionSet
        {
            {"h|help","Help", v =>
                {
                    Operation=TypeOperation.Help;
                } },
            {"i|install:", "Instalar sistema a partir do arquivo de configuração", v =>
            {
                Operation = TypeOperation.Install;
                SetupFile = v;
            } },
            {"r|run:","Executar sistema a partir da pasta de configuração", v =>
            {
                Operation = TypeOperation.Run;
                if (string.IsNullOrEmpty(v))
                    v=Directory.GetCurrentDirectory();
                setupFolder = new SetupFolder(v);
                SetupFile = v;
            } },
            {"u|update:","Atualizar sistema", v=>
            {
                Operation = TypeOperation.Update;
                SetupFile = v;
            } },
            {"v|verify:", "Verificar arquivos do sistema", v =>
            {
                Operation = TypeOperation.Verify;
                SetupFile = v;
            } },
            {"stats:","Estatísticas de execução", v =>
            {
                Operation = TypeOperation.Stats;
                SetupFile = v;
            } },
            {"nogui","Informações de processamento via console", v=>
            {
                Output.NoGUI = true;
            }}
        };

        public static TypeOperation Operation { get; private set; }
        public static string SetupFile { get; private set; }
        public static Form OperationForm = null;
        public static SetupFolder setupFolder { get; private set; }

        /// <summary>
        /// Tratar os parâmetros da linha de comando
        /// </summary>
        /// <param name="args"></param>
        public static void Parse(params string[] args)
        {
            List<string> extra;
            try
            {
                extra = Options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("Parâmetro inválido: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Execute com o parâmetro  `--help' para mais informações.");
                return;
            }

            switch (Operation)
            {
                case TypeOperation.Verify:
                    DoOperations.Verify(SetupFile);
                    break;
                case TypeOperation.Run:
                    DoOperations.Run(SetupFile);
                    break;
                case TypeOperation.Stats:
                    DoOperations.Stats(SetupFile);
                    break;
                default:
                    StringWriter m = new StringWriter();
                    Options.WriteOptionDescriptions(m);
                    OperationForm = DoOperations.ShowHelp(m.ToString());
                    break;
            }


        }

    }
}
