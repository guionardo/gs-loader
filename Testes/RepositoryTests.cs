using gs_loader_common.Base;
using gs_loader_common.Hosts;
using gs_loader_common.Programs;
using gs_loader_common.Repository;
using gs_loader_common.Telemetry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes
{
    [TestClass]
    [TestCategory("Repository")]
    public class RepositoryTests
    {
        [TestMethod]
        public void RepositoryAssign()
        {
            RepositoryInfo ri = new RepositoryInfo
            {
                Proprietary = "Guionardo"
            };
            Console.WriteLine(ri);

            RepositoryInfo r2 = new RepositoryInfo();
            Console.WriteLine(r2);
            Console.WriteLine("Assign");
            r2.Assign(ri);
            Console.WriteLine(r2);
        }

        [TestMethod]
        public void RepositoryInfo()
        {
            var r1 = new RepositoryInfo("http://repositorio.info");
            Assert.IsTrue(r1.RepositoryType == RepositoryType.HTTP);

            r1 = new RepositoryInfo(@"file://B:\REPOSITORIO");
            Assert.IsTrue(r1.RepositoryType == RepositoryType.Folder);

            r1 = new RepositoryInfo(@"B:\REPOSITORIO");
            Assert.IsTrue(r1.RepositoryType == RepositoryType.Folder);
        }

        [TestMethod]
        public void ValidPaths()
        {
            Assert.IsTrue(IO.IsValidFilename(@"A:\TEMP"));
            Assert.IsTrue(IO.IsValidFilename(@"B:\TEMP\TEMP2"));
            Assert.IsFalse(IO.IsValidFilename(@"BX:\TEMPORARIO\?"));

            foreach (var repo in new string[]
            {
                @"A:\TEMP",
                @"\\TIME-SERVER\T",
                @"http://repositorio/teste",
                @"https://repositorio2/teste2"
            }) Console.WriteLine(repo + " = " + Host.FromName(repo));


        }

        [TestMethod]
        public void RepositorySave()
        {
            RepositoryInfo ri = new RepositoryInfo
            {
                Proprietary = "Guionardo",
                RepositoryName = "Repositório Teste"
            };
            ri.Save("repository.json");
        }

        [TestMethod]
        public void RepositoryLoad()
        {
            RepositoryInfo ri = new RepositoryInfo("repository.json");
            Console.WriteLine(ri);
        }

        [TestMethod]
        public void Telemetry()
        {
            TelemetryMessage tm = new TelemetryMessage();
            Console.WriteLine(TelemetryMessage.DefaultMachineID);
        }

        [TestMethod]
        public void TestFingerPrint()
        {
            Console.WriteLine(FingerPrint.FingerPrintInfos());
            Console.WriteLine(FingerPrint.Value());
        }

        [TestMethod]
        public void TestFileEntry()
        {
            FileEntry f = new FileEntry("A:\\TBYTE\\DISTRO\\TBYTE.EXE", "A:\\TBYTE\\DISTRO");
            Console.WriteLine(f);
        }
    }

}
