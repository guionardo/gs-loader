﻿using gs_loader.Operations;
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
            {"s|setup:", "Criar/alterar arquivo de configuração", v =>
            {
                Operation = TypeOperation.Setup;
                SetupFile = v;
            } },
            {"r|run:","Executar sistema a partir do arquivo de configuração", v =>
            {
                Operation = TypeOperation.Run;
                SetupFile = v;
            } },
            {"u|update:","Atualizar sistema", v=>
            {
                Operation = TypeOperation.Update;
                SetupFile = v;
            } }
        };

        public static TypeOperation Operation { get; private set; }
        public static string SetupFile { get; private set; }
        public static Form OperationForm = null;

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
                case TypeOperation.Setup:
                    OperationForm = DoOperations.Setup(SetupFile);
                    break;
                case TypeOperation.Run:
                    DoOperations.Run(SetupFile);
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
