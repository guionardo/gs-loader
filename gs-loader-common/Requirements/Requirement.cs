using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gs_loader_common.Requirements
{
    public class Requirement
    {
        public readonly string Name;
        public readonly string URL;
        public readonly bool Installable;
        public readonly string Arguments;

        public Requirement(string name, string uRL, bool installable, string arguments)
        {
            Name = name;
            URL = uRL;
            Installable = installable;
            Arguments = arguments;
        }

        static Dictionary<string, Requirement> requirements = new Dictionary<string, Requirement>();

        static Requirement()
        {
            // https://github.com/VFPX/VFPRuntimeInstallers
            requirements.Add("vfp7", new Requirement("Visual Fox Pro 7", "https://github.com/VFPX/VFPRuntimeInstallers/blob/master/VFP7SP1RT.exe", true, "/S"));
            requirements.Add("vfp8", new Requirement("Visual Fox Pro 8", "https://github.com/VFPX/VFPRuntimeInstallers/blob/master/VFP8SP1RT.exe", true, "/S"));
            requirements.Add("vfp9", new Requirement("Visual Fox Pro 9", "https://github.com/VFPX/VFPRuntimeInstallers/blob/master/VFP9SP2RT.exe", true, "/S"));
        }
    }
}
