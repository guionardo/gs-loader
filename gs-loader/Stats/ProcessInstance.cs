using LiteDB;
using System;

namespace gs_loader.Stats
{
    /// <summary>
    /// Registro de instância executada de um processo
    /// </summary>
    public class ProcessInstance
    {
        public ObjectId ProcessInstanceId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FileName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ExitCode { get; set; }

        public override string ToString() =>
            ProcessInstanceId.ToString()+" "+
            (Name ?? "").PadRight(20).Substring(0, 20) + " " +
            (Version ?? "0.0.0.0").PadRight(10).Substring(0, 10) + " " +
            StartTime.ToString() + " -> " + EndTime.ToString() + " = " +
            ExitCode.ToString("D3") + " " +
            (FileName ?? "");

    }
}
