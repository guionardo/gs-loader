using gs_loader_common.Base;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace gs_loader.Stats
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public static class DoStats
    {
        const string Instances = "instances";
        static readonly string DBFileName;
        static DoStats()
        {
            string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "GSLoader", "db");
            if (!IO.MakeFolder(basePath))
            {
                Log.Add("ERRO AO CRIAR " + basePath + " [" + IO.LastError + "]", "EXCEPTION");
                basePath = Path.Combine(Directory.GetCurrentDirectory(), "GSLoader", "db");
                if (!IO.MakeFolder(basePath))
                {
                    Log.Add("ERRO AO CRIAR " + basePath + " [" + IO.LastError + "]", "EXCEPTION");
                    basePath = "";
                }
            }

            DBFileName = !string.IsNullOrEmpty(basePath) ? Path.Combine(basePath, "gsloader.db") : "";
            if (string.IsNullOrEmpty(DBFileName))
                Log.Add("DATABASE INDISPONÍVEL", "EXCEPTION");
            else
            {
                BsonMapper.Global.Entity<ProcessInstance>()
                    .Id(x => x.ProcessInstanceId)
                    .Field(x => x.Name, "Name")
                    .Field(x => x.StartTime, "StartTime")
                    .Field(x => x.EndTime, "EndTime")
                    .Field(x => x.FileName, "FileName")
                    .Field(x => x.ExitCode, "ExitCode")
                    .Field(x => x.Version, "Version");

                using (var db = new LiteDatabase(DBFileName))
                {
                    var instances = GetCollection(db);
                }
            }

        }

        public static bool ListAllProcesses(List<ProcessInstance> processes)
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

        public static bool ListInstances(string processName, ref List<ProcessInstance> processInstances, string version = null)
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
                        var pi = instances.Find(x => x.Name.Equals(processName,StringComparison.InvariantCultureIgnoreCase) && x.Version == version);
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

        public static bool ListUniqueProcesses(ref List<string> uniques)
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

        public static bool RegisterProcess(ProcessInstance instance)
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

        private static LiteCollection<ProcessInstance> GetCollection(LiteDatabase db)
        {
            var instances = db.GetCollection<ProcessInstance>(Instances);
            instances.EnsureIndex("Name");
            return instances;
        }

        public static bool RegisterProcess(Process process)
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
                    Version = gs_loader_common.Setup.Version.FromFile(Path.Combine(process.StartInfo.WorkingDirectory, process.StartInfo.FileName)).ToString(),
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

        static bool CheckDB()
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
