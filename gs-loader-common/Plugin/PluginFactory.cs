using gs_loader_common.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace gs_loader_common.Plugin
{
    public static class PluginFactory
    {
        public static IPlugin GetPlugin(string pluginsFolder)
        {
            if (string.IsNullOrEmpty(pluginsFolder) || !Directory.Exists(pluginsFolder))
                return new DefaultPlugin();

            var dlls = Directory.GetFiles(pluginsFolder, "*.dll");
            if (dlls.Length == 0)
                return new DefaultPlugin();

            foreach (var dll in dlls)
                try
                {
                    Assembly asm = AppDomain.CurrentDomain.Load(dll);
                    foreach (Type t in asm.GetTypes())
                        foreach (Type iface in t.GetInterfaces())
                            if (iface.Equals(typeof(IPlugin)))
                                return (IPlugin)Activator.CreateInstance(t);
                }
                catch (Exception e)
                {

                }
            return new DefaultPlugin();
        }
    }
}
