using System;
using gs_loader.Arguments;
using gs_loader.Setup;
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
    }
}
