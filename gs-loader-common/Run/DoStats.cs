using gs_loader_common.Base;
using gs_loader_common.Programs;
using gs_loader_common.Resources;
using gs_loader_common.Stats;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace gs_loader_common.Run
{
    public class DoStats
    {
        const string Instances = "instances";

        readonly string DBFileName;

        static DoStats()
        {
            BsonMapper.Global.Entity<ProcessInstance>()
                    .Id(x => x.ProcessInstanceId)
                    .Field(x => x.Name, "Name")
                    .Field(x => x.StartTime, "StartTime")
                    .Field(x => x.EndTime, "EndTime")
                    .Field(x => x.FileName, "FileName")
                    .Field(x => x.ExitCode, "ExitCode")
                    .Field(x => x.Version, "Version");
        }
        /// <summary>
        /// Cria objeto DoStats com um caminho para o arquivo .db de estatísticas
        /// </summary>
        /// <param name="statsPath"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public DoStats(string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath))
                throw new ArgumentNullException("dbPath");

            string statsPath = Path.GetDirectoryName(dbPath);

            if (!IO.MakeFolder(statsPath))
                throw new DirectoryNotFoundException(Strings.Get(StringName.DirectoryNotFound, "DIR", statsPath));

            DBFileName = dbPath;


        }

        public static bool Stats(string basePath, out string message)
        {
            message = "";

            if (string.IsNullOrEmpty(basePath) ||
            !Directory.Exists(basePath))
            {
                message = "Parâmetro basePath vazio";
                return false;
            }

            Program program;
            bool success = false;

            message = "";
            try
            {
                program = Program.FromInstalledFolder(basePath);
                success = program.Verify(basePath, out message);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return success;

            /*
            System.Collections.Generic.List<ProcessInstance> processInstances = null;
            if (DoStats.ListInstances(_setupData.Executable.File, ref processInstances))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Estatísticas: " + _setupData.Executable.File);
                sb.AppendLine("Número de execuções: " + processInstances.Count);
                foreach (var pi in processInstances)
                {
                    sb.AppendLine(pi.ToString());
                }
                Output.MultipleMessage(sb.ToString(), false, 0);
                Arguments.TreatArguments.OperationForm = OutputMultipleMessage.CurrentForm;
            }*/

        }

        public bool ListAllProcesses(List<ProcessInstance> processes)
        {
            if (!CheckDB()) return false;
            try
            {
                using (var db = new LiteDatabase(DBFileName))
                {
                    var instances = GetCollection(db);

                    var procs = instances.FindAll();
                    processes.Clear();
                    processes.AddRange(procs);
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Add("ListAllProcesses: " + e.Message, "EXCEPTION");
            }
            return false;
        }

        public bool ListInstances(string processName, ref List<ProcessInstance> processInstances, string version = null)
        {
            if (!CheckDB() || string.IsNullOrEmpty(processName))
                return false;
            try
            {
                using (var db = new LiteDatabase(DBFileName))
                {
                    var instances = GetCollection(db);

                    processInstances = new List<ProcessInstance>();
                    processName = processName.ToUpperInvariant();

                    if (string.IsNullOrEmpty(version))
                    {
                        var pi = instances.Find(x => x.Name.Equals(processName, StringComparison.InvariantCultureIgnoreCase));
                        processInstances.AddRange(pi);
                    }
                    else
                    {
                        var pi = instances.Find(x => x.Name.Equals(processName, StringComparison.InvariantCultureIgnoreCase) && x.Version == version);
                        processInstances.AddRange(pi);
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Add("ListInstances: " + e.Message, "EXCEPTION");
            }
            return false;
        }

        public bool ListUniqueProcesses(ref List<string> uniques)
        {
            List<ProcessInstance> processes = new List<ProcessInstance>();
            if (!ListAllProcesses(processes))
                return false;

            uniques = new List<string>();
            foreach (var p in processes)
                if (!uniques.Contains(p.Name))
                    uniques.Add(p.Name);

            return true;
        }

        public bool RegisterProcess(ProcessInstance instance)
        {
            if (!CheckDB()) return false;
            if (instance == null)
            {
                Log.Add("RegisterProcess: INSTANCIA NULL", "ERROR");
                return false;
            }

            try
            {
                using (var db = new LiteDatabase(DBFileName))
                {
                    var instances = GetCollection(db);
                    var docId = instances.Insert(instance);
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Add("RegisterProcess: " + e.Message, "EXCEPTION");
            }
            return false;

        }

        public bool RegisterProcess(Process process)
        {
            if (!CheckDB()) return false;
            if (process == null)
            {
                Log.Add("RegisterProcess: PROCESS NULL", "ERROR");
                return false;
            }
            try
            {
                Random r = new Random();

                var instance = new ProcessInstance
                {
                    ProcessInstanceId = new ObjectId((int)DateTime.Now.ToFileTime(), 0, (short)process.Id, r.Next()),
                    Name = Path.GetFileNameWithoutExtension(process.StartInfo.FileName).ToUpperInvariant(),
                    FileName = Path.Combine(process.StartInfo.WorkingDirectory, process.StartInfo.FileName),
                    Version = Base.Version.FromFile(Path.Combine(process.StartInfo.WorkingDirectory, process.StartInfo.FileName)).ToString(),
                    StartTime = process.StartTime,
                    EndTime = process.ExitTime,
                    ExitCode = process.ExitCode
                };
                return RegisterProcess(instance);
            }
            catch (Exception e)
            {
                Log.Add("RegisterProcess: " + e.Message, "EXCEPTION");
            }
            return false;
        }

        private static LiteCollection<ProcessInstance> GetCollection(LiteDatabase db)
        {
            var instances = db.GetCollection<ProcessInstance>(Instances);
            instances.EnsureIndex("Name");
            return instances;
        }

        bool CheckDB()
        {
            if (string.IsNullOrEmpty(DBFileName))
            {
                Log.Add("CheckDB: DATABASE INDISPONÍVEL", "ERROR");
                return false;
            }
            return true;
        }
    }
}
