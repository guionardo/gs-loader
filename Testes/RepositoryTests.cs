using gs_loader_common.Base;
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
    }
    
}
