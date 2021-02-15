using System;

namespace Workbench.Runtime.Config
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class RuntimeConfigurationAttribute : Attribute
    {
        public string ConfigFile { get; set; }
        
        public RuntimeConfigurationAttribute() { }
    }
}
