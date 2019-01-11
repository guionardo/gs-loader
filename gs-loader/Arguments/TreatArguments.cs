using gs_loader_common.Base;
using gs_loader_common.Resources;
using gs_loader_common.Run;
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
            {"h|help",Strings.Get(StringName.OptionHelp), v =>
                {
                    Operation=TypeOperation.Help;
                } },
            {"r|run:",Strings.Get(StringName.OptionRun), v =>
            {
                Operation = TypeOperation.Run;
                if (string.IsNullOrEmpty(v))
                    v=Directory.GetCurrentDirectory();
                setupFolder = new SetupFolder(v);
                Program = gs_loader_common.Programs.Program.FromInstalledFolder(v);
                SetupPath = v;
                SetupFile = v;
            } },
            {"i|install:", Strings.Get(StringName.OptionInstall), v =>
            {
                Operation = TypeOperation.Install;
                SetupFile = v;
            } },
            {"t|repositoryhost=", Strings.Get(StringName.OptionHost), v =>
            {
                RepositoryHost = v;
            } },
            {"p|program=",Strings.Get(StringName.OptionProgram), v =>
            {
            ProgramName = v;
            } },
            {"u|update:", Strings.Get(StringName.OptionUpdate), v=>
            {
                Operation = TypeOperation.Update;
                SetupFile = v;
            } },
            {"v|verify:", Strings.Get(StringName.OptionVerify), v =>
            {
                Operation = TypeOperation.Verify;
                SetupFile = v;
            } },
            {"stats:", Strings.Get(StringName.OptionStats), v =>
            {
                Operation = TypeOperation.Stats;
                SetupFile = v;
            } },
            {"s|setup:", Strings.Get(StringName.OptionSetup), v =>
            {
                Operation = TypeOperation.Setup;
                SetupPath = v;
            } }
        };

        public static gs_loader_common.Programs.Program Program { get; private set; }

        private static string SetupPath;

        public static TypeOperation Operation { get; private set; }
        public static string SetupFile { get; private set; }
        public static Form OperationForm = null;
        public static SetupFolder setupFolder { get; private set; }
        public static string RepositoryHost { get; private set; }
        public static string ProgramName { get; private set; }

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

                switch (Operation)
                {
                    case TypeOperation.Run:
                        DoRun.Run(SetupPath, out string message);
                        break;

                    case TypeOperation.Verify:
                        DoVerify.Verify(SetupPath, out message);
                        break;

                    case TypeOperation.Stats:
                        DoStats.Stats(SetupPath, out message);
                        break;

                    case TypeOperation.Install:
                        if (string.IsNullOrEmpty(RepositoryHost))
                        {
                            throw new Exception(Strings.Get(StringName.OptionErrorRepository));
                        }
                        if (string.IsNullOrEmpty(ProgramName))
                        {
                            throw new Exception(Strings.Get(StringName.OptionErrorProgram));
                        }
                        DoInstall.Install(SetupPath, RepositoryHost, ProgramName, out message);
                        break;

                    case TypeOperation.Update:

                        break;

                    default:
                        StringWriter m = new StringWriter();
                        Options.WriteOptionDescriptions(m);
                        OperationForm = DoHelp.ShowHelp(m.ToString());
                        break;
                }
            }
            catch (OptionException e)
            {
                Console.WriteLine(Strings.Get(StringName.OptionErrorParameter)
                    .Replace("%MESSAGE%", e.Message));
                return;
            }


        }

    }
}
