using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace gs_loader_common.Base
{
    /// <summary>
    /// Gera um identificador único para a máquina
    /// </summary>
    /// <remarks>https://www.codeproject.com/Articles/28678/Generating-Unique-Key-Finger-Print-for-a-Computer</remarks>
    public class FingerPrint
    {
        /// <summary>
        /// Hash itens to collect
        /// </summary>
        public static HashItem HashItens = HashItem.CPU | HashItem.BIOS | HashItem.Base | HashItem.Video;

        static string _baseId;

        static string _biosId;

        static string _cpuId;

        static string _diskId;

        static string _macId;

        static string _videoId;

        /// <summary>
        /// Dicionário para cache das consultas WMI
        /// </summary>
        static Dictionary<string, ManagementObjectCollection> mgmCols = new Dictionary<string, ManagementObjectCollection>();
        [Flags]
        public enum HashItem
        {
            CPU = 1,
            BIOS = 2,
            Base = 4,
            Disk = 8,
            Video = 16,
            MAC = 32
        }

        /// <summary>
        /// Texto com as informações capturadas da WMI segundo o filtro em HashItens
        /// </summary>
        /// <returns></returns>
        public static string FingerPrintInfos() => (HashItens.HasFlag(HashItem.CPU) ? $"CPU >> {CPUId()}\n" : "") +
                    (HashItens.HasFlag(HashItem.BIOS) ? $"BIOS >> {BIOSId()}\n" : "") +
                    (HashItens.HasFlag(HashItem.Base) ? $"BASE >> { BaseId()}\n" : "") +
                    (HashItens.HasFlag(HashItem.Disk) ? $"DISK >> {DiskId()}\n" : "") +
                    (HashItens.HasFlag(HashItem.Video) ? $"VIDEO >> {VideoId()}\n" : "") +
                    (HashItens.HasFlag(HashItem.MAC) ? $"MAC >> {MACId()} \n" : "");

        /// <summary>
        /// Hash das informações capturadas
        /// </summary>
        /// <returns></returns>
        public static string Value() => GetHash(FingerPrintInfos());

        /// <summary>
        /// Motherboard ID
        /// </summary>
        /// <returns></returns>
        static string BaseId()
        {
            if (string.IsNullOrEmpty(_baseId))
                _baseId = Identifier("Win32_BaseBoard", "Model") +
                Identifier("Win32_BaseBoard", "Manufacturer") +
                Identifier("Win32_BaseBoard", "Name") +
                Identifier("Win32_BaseBoard", "SerialNumber");
            return _baseId;
        }

        /// <summary>
        /// BIOS Identifier
        /// </summary>
        /// <returns></returns>
        static string BIOSId()
        {
            if (string.IsNullOrEmpty(_biosId))
                _biosId = Identifier("Win32_BIOS", "Manufacturer") +
                Identifier("Win32_BIOS", "SMBIOSBIOSVersion") +
                Identifier("Win32_BIOS", "IdentificationCode") +
                Identifier("Win32_BIOS", "SerialNumber") +
                Identifier("Win32_BIOS", "ReleaseDate") +
                Identifier("Win32_BIOS", "Version");
            return _biosId;
        }

        /// <summary>
        /// CPU Identifier
        /// </summary>
        /// <returns></returns>
        static string CPUId()
        {
            if (string.IsNullOrEmpty(_cpuId))
            {
                //Uses first CPU identifier available in order of preference
                //Don't get all identifiers, as it is very time consuming
                _cpuId = Identifier("Win32_Processor", "UniqueId");
                if (_cpuId == "") //If no UniqueID, use ProcessorID
                {
                    _cpuId = Identifier("Win32_Processor", "ProcessorId");
                    if (_cpuId == "") //If no ProcessorId, use Name
                    {
                        _cpuId = Identifier("Win32_Processor", "Name");
                        if (_cpuId == "") //If no Name, use Manufacturer
                        {
                            _cpuId = Identifier("Win32_Processor", "Manufacturer");
                        }
                        //Add clock speed for extra security
                        _cpuId += Identifier("Win32_Processor", "MaxClockSpeed");
                    }
                }
            }
            return _cpuId;
        }

        /// <summary>
        /// Main physical hard drive ID
        /// </summary>
        /// <returns></returns>
        static string DiskId()
        {
            if (string.IsNullOrEmpty(_diskId))
                _diskId = Identifier("Win32_DiskDrive", "Model") +
            Identifier("Win32_DiskDrive", "Manufacturer") +
            Identifier("Win32_DiskDrive", "Signature") +
            Identifier("Win32_DiskDrive", "TotalHeads");
            return _diskId;
        }

        /// <summary>
        /// Get cached collection from WMI query
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <returns></returns>
        static ManagementObjectCollection GetCollection(string wmiClass)
        {
            ManagementObjectCollection mgc = null;
            if (mgmCols.ContainsKey(wmiClass))
                mgc = mgmCols[wmiClass];
            else
            {
                mgc = new ManagementClass(wmiClass).GetInstances();
                mgmCols.Add(wmiClass, mgc);
            }
            return mgc;
        }


        static string GetHash(string s)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(new ASCIIEncoding().GetBytes(s));
                string r = string.Empty;
                foreach (var b in hash)
                    r += b.ToString("X2");

                return r;
            }
        }

        /// <summary>
        /// Return a hardware identifier
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <param name="wmiMustBeTrue"></param>
        /// <returns></returns>
        static string Identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue = null)
        {
            string result = "";
            foreach (ManagementObject mo in GetCollection(wmiClass))
                if (string.IsNullOrEmpty(wmiMustBeTrue) || mo[wmiMustBeTrue].ToString() == "True")
                    //Only get the first one
                    if (result == "")
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch { }

            return result;
        }
        /// <summary>
        /// First enabled network card ID
        /// </summary>
        /// <returns></returns>
        static string MACId()
        {
            if (string.IsNullOrEmpty(_macId))
                _macId = Identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
            return _macId;
        }


        /// <summary>
        /// Primary video controller ID
        /// </summary>
        /// <returns></returns>
        static string VideoId()
        {
            if (string.IsNullOrEmpty(_videoId))
                _videoId = Identifier("Win32_VideoController", "DriverVersion") + Identifier("Win32_VideoController", "Name");

            return _videoId;
        }
    }
}

