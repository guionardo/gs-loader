using gs_loader_common.Base;
using System;

namespace gs_loader_common.Telemetry
{
    /// <summary>
    /// Mensagem para telemetria
    /// </summary>
    public class TelemetryMessage
    {
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public TelemetryMessageType TelemetryMessageType { get; set; } = TelemetryMessageType.None;

        /// <summary>
        /// Nome do programa
        /// </summary>
        public string ProgramName { get; set; }

        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// Nome da estação de trabalho
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// Fingerprint da máquina
        /// </summary>
        public string MachineID { get; set; }

        /// <summary>
        /// Conteúdo da mensagem
        /// </summary>
        public string MessageText { get; set; }

        public string[] FileAttachment { get; set; }

        /// <summary>
        /// Nome da estação de trabalho atual
        /// </summary>
        public static readonly string DefaultStationName;
        public static readonly string DefaultClientName;
        public static readonly string DefaultMachineID;

        static TelemetryMessage()
        {
            // Obtendo MachineID
            DefaultMachineID = FingerPrint.Value();

            DefaultStationName = Environment.GetEnvironmentVariable("COMPUTERNAME");
        }

        /// <summary>
        /// Retorna a mensagem em uma string de uma linha, para registro em arquivo de log (CSV)
        /// </summary>
        /// <returns></returns>
        public string TextToLog()
        {
            string ValidString(string s)
            {
                string r = "";
                foreach (char c in s)
                    if (c >= '\x0020')  // Caracteres visíveis
                        r += c;
                    else if (c == '\x000D') // CR
                        r += "\\r";
                    else if (c == '\x000A') // LF
                        r += "\\n";
                    else if (c == '\x0009') // TAB
                        r += "\\t";
                return r;

            }
            return String.Format("{0:s},\"{1}\",\"{2}\"",
                this.DateTime,
                ValidString(ProgramName),
                ValidString(ClientName),
                ValidString(MachineID));
        }


    }
}
