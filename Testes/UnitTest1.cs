using System;
using System.Diagnostics;
using gs_loader.Arguments;
using gs_loader.Base;
using gs_loader.Forms;
using gs_loader.Run;
using gs_loader.Setup;
using gs_loader.Stats;
using gs_loader.Update;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ArgumentoHelp()
        {
            TreatArguments.Parse("--help");

        }

        [TestMethod]
        public void ArgumentoRun()
        {
            TreatArguments.Parse("--run");
            TreatArguments.Parse(@"--run=A:\TEMP\TESTE.JSON");
        }

        [TestMethod]
        public void SetupLoadAndSave()
        {
            SetupData setup = new SetupData
            {
                Executable = new SetupFile(@"A:\TBYTE\TBYTE.EXE")
            };
            setup.Files.Add(new SetupFile(@"A:\TBYTE\TBYTE.EXE"));
            setup.Files.Add(new SetupFile(@"A:\TBYTE\BEMAFI32.DLL"));

            Console.WriteLine(SetupData.Write(@"A:\TEMP", setup, out string msg));


            Console.WriteLine(msg);

            Console.WriteLine(SetupData.Read(@"A:\TBYTE", out SetupData setup2, out msg));
            Console.WriteLine(msg);
        }

        [TestMethod]
        public void SetupCreate()
        {
            Console.WriteLine(SetupData.Create(@"A:\TBYTE", out SetupData setup, out string msg));
            Console.WriteLine(msg);
            SetupData.Write(@"A:\TBYTE", setup, out msg);
            Console.WriteLine(msg);
        }
        [TestMethod]
        public void Run()
        {
            var setupData = new SetupFile(@"A:\TEMP\TESTE.CMD", @"A:\TEMP");
            DoRun.Run(new SetupData { Executable = setupData, Arguments = "ARGUMENTO 1" }, @"A:\TEMP", out string message);
            Console.WriteLine(message);
        }

        [TestMethod]
        public void CacheFolder()
        {
            SetupData.Create(@"A:\TBYTE", out SetupData setup, out string msg);
            Console.WriteLine(IO.CacheFolder(setup, true));

        }

        [TestMethod]
        public void Versions()
        {
            Assert.IsTrue(new gs_loader.Setup.Version("10.0.2.1").CompareTo("9.2.3.9") > 0);
            Assert.IsTrue(new gs_loader.Setup.Version("10.0.2.1").CompareTo("11.2.3.9") < 0);
            Assert.IsTrue(new gs_loader.Setup.Version("10.0.1.1").CompareTo("10.0.2.1") < 0);
        }

        [TestMethod]
        public void Backup()
        {
            SetupData.Create(@"A:\TBYTE", out SetupData setup, out string msg);
            DoUpdate.Backup(setup, @"A:\TBYTE");
        }

        [TestMethod]
        public void InstanciasExecutando()
        {
            //Process.Start("notepad.exe");
            Assert.IsTrue(DoRun.InstancesRunning("notepad.exe"));
        }

        [TestMethod]
        public void ListarInstancias()
        {
            System.Collections.Generic.List<ProcessInstance> procs = new System.Collections.Generic.List<ProcessInstance>();
            if (DoStats.ListAllProcesses(procs))
            {
                foreach (var p in procs)
                    Console.WriteLine(p);
            }

            System.Collections.Generic.List<string> uniques = null;
            if (DoStats.ListUniqueProcesses(ref uniques))
            {
                foreach (var u in uniques)
                    Console.WriteLine(u);
            }



            System.Collections.Generic.List<ProcessInstance> instances = null;
            if (DoStats.ListInstances("notepad", ref instances))
            {
                foreach (var i in instances)
                    Console.WriteLine(i);
            }

        }

        [TestMethod]
        public void SetupForm()
        {
            SetupData setupData = new SetupData();
            using (var sf = new SetupForm(setupData))
            {
                sf.ShowDialog();
            }
        }

    }
}
