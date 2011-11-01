using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using BlueVex.Core;

namespace BlueVex2
{
    public static class PlugInManager
    {
        private static string PlugInLookupPath = "Plugins";

        public static List<Plugin> AvailablePlugins;

        public static void FindAvailablePlugIns()
        {
            AvailablePlugins = new List<Plugin>();
            string pathPlugIns = Path.Combine(Application.StartupPath, PlugInManager.PlugInLookupPath);
            // If there is a plugins folder
            if (System.IO.Directory.Exists(pathPlugIns))
            {
                string[] dllPaths;
                Assembly contractAssembly;
                // Finds all dll's in the plugins directory
                dllPaths = Directory.GetFileSystemEntries(pathPlugIns, "*.dll");
                foreach (string dllPath in dllPaths)
                {
                    // load the dll
                    contractAssembly = Assembly.LoadFrom(dllPath);
                    // find a valid entry point getting its name
                    string typeName = PlugInManager.FindValidEntryPoint(contractAssembly);
                    // name will be nothing if no entry point found
                    if (!string.IsNullOrEmpty(typeName))
                    {
                        // Add the plugin to the availble plugins collection
                        AvailablePlugins.Add(new Plugin() { Assembly = contractAssembly, TypeName = typeName });
                    }
                }
            }
        }
        
        private static string FindValidEntryPoint(Assembly objDLL)
        {
            string entryPoint = string.Empty;
            // Loop through each type in the DLL 
            foreach (Type dllType in objDLL.GetTypes())
            {
                if (dllType.IsPublic == true &&
                !((dllType.Attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract))
                {
                    Type dllInterface = dllType.GetInterface(typeof(IPlugin).Name, false);
                    if (dllInterface != null)
                    {
                        // get the name of the class we need to create
                        entryPoint = dllType.FullName;
                        break;
                    }
                }
            }
            return entryPoint;
        }
    }

    public class Plugin
    {
        public Assembly Assembly { get; set; }
        public string TypeName { get; set; }
        public IPlugin CreateInstance()
        {
            // Create and return class instance 
            object instance = Assembly.CreateInstance(TypeName);
            return (IPlugin)instance;
        }
    }
}
