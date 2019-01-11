using System;
using System.Diagnostics;
using gs_loader.Arguments;
using gs_loader_common.Base;
using gs_loader_common.Forms;
using gs_loader_common.Run;
using gs_loader_common.Setup;
using gs_loader_common.Update;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes
{
    [TestClass]
    public class UnitTest1
    {

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
        public void CacheFolder()
        {
            SetupData.Create(@"A:\TBYTE", out SetupData setup, out string msg);
            Console.WriteLine(IO.CacheFolder(setup, true));
        }

        [TestMethod]
        public void Versions()
        {
            Assert.IsTrue(new gs_loader_common.Base.Version("10.0.2.1").CompareTo("9.2.3.9") > 0);
            Assert.IsTrue(new gs_loader_common.Base.Version("10.0.2.1").CompareTo("11.2.3.9") < 0);
            Assert.IsTrue(new gs_loader_common.Base.Version("10.0.1.1").CompareTo("10.0.2.1") < 0);
        }

        [TestMethod]
        public void Backup()
        {
            SetupData.Create(@"A:\TBYTE", out SetupData setup, out string msg);
            gs_loader_common.Update.DoUpdate.Backup(setup, @"A:\TBYTE");
        }



        [TestMethod]
        public void TestSetupFolder()
        {
            SetupFolder sf = new SetupFolder(".");
            Console.WriteLine(sf.SetupPath);
        }

        [TestMethod]
        public void Enums()
        {
            SetupFileFlags s = new SetupFileFlags();
            Console.WriteLine(s);
            s |= SetupFileFlags.Icon | SetupFileFlags.MainExecutable;
            Console.WriteLine(s);
            s &= ~SetupFileFlags.Icon;
            Console.WriteLine(s);
            s &= ~SetupFileFlags.Icon;
            Console.WriteLine(s);
        }

        [TestMethod]
        public void TesteAcentos()
        {
            string teste = "?:*Teste&ªºcom ção";
            Console.WriteLine(teste);
            Console.WriteLine(IO.ValidFileName(teste));
        }

    }
}
