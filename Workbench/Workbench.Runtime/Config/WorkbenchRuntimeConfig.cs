using System;
using System.Configuration;
using System.IO;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Services;

namespace Workbench.Runtime.Config
{
    public sealed class WorkbenchRuntimeConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("WorkbenchDirectory", DefaultValue ="Workbench", IsRequired = true)]
        public string WorkbenchDirectory => (string)this["WorkbenchDirectory"]; 

    }

    public class WorkbenchRuntimeConfig
    {
        private readonly ILoggingService _loggingService;
        public WorkbenchRuntimeConfig(IDependencyInjectionEngine di)
        {
            _loggingService = di.Get<ILoggingService>();
        }

        public void Load(RuntimeConfigurationAttribute configAttribute)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configDirectory = Path.Combine(baseDirectory, configAttribute.ConfigFile);
            if (!File.Exists(configDirectory)) return;
            //Parse config
        }
    }
}
