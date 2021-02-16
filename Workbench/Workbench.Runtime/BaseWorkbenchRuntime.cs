using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Services;
using Workbench.Runtime.Config;
using Workbench.Runtime.Plugin;

namespace Workbench.Runtime
{
    public abstract class BaseWorkbenchRuntime : IWorkbenchRuntime
    {
        public WorkbenchRuntimeConfig Config { get; private set; }

        protected readonly ILoggingService _loggingService;
        protected readonly IDirectoryService _directoryService;
        protected readonly IDependencyInjectionEngine _dependencyInjectionEngine;

        private List<IWorkbenchPlugin> _loadedPlugins = new List<IWorkbenchPlugin>();

        public BaseWorkbenchRuntime(IDependencyInjectionEngine di)
        {
            _dependencyInjectionEngine = di;

            di.Inject(ref _loggingService);
            di.Inject(ref _directoryService);

            Config = new WorkbenchRuntimeConfig(di);
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual void Start()
        {
            _loggingService.Info("Welcome to Workbench");
            PrintVersionInfo();
            _loggingService.Info("Loading configuration...");
            LoadConfig();
            _loggingService.Info("Loading plugins...");
            LoadPlugins();
            _loggingService.Info(string.Format("{0} plugins loaded!", _loadedPlugins.Count));
        }

        public void LoadConfig()
        {
            RuntimeConfigurationAttribute assemblyConfigurationAttribute =
                WorkbenchRuntime.RuntimeAssembly.GetCustomAttributes(typeof(RuntimeConfigurationAttribute), false).
                Cast<RuntimeConfigurationAttribute>().
                FirstOrDefault();

            if (assemblyConfigurationAttribute == null)
            {
                throw new InvalidOperationException("Must supply workbench assembly a configuration attribute");
            }

            Config.Load(assemblyConfigurationAttribute);
            _loggingService.Debug(string.Format("Workbench directory: {0}, Workbench plugins directory: {0}",
                Config.WorkbenchDirectory, Config.WorkbenchPluginsDirectory));
        }

        public void LoadPlugins()
        {
            var pluginAssemblies = _directoryService.GetAllFilesInDirectory(Config.WorkbenchPluginsDirectory)
                .Where(fi => fi.Extension.Equals(".dll"))
                .Select(fi => Assembly.LoadFrom(fi.FullName))
                .Where(a => a.GetCustomAttribute(typeof(WorkbenchPluginAttribute)) != null)
                .ToList();

            foreach (var assembly in pluginAssemblies)
            {
                _loggingService.Info(string.Format("{0} loaded.", assembly.FullName));
                _dependencyInjectionEngine.GenerateAssemblyBindingsForType<IWorkbenchPlugin>(assembly);
            }

            _loadedPlugins.AddRange(_dependencyInjectionEngine.GetAll<IWorkbenchPlugin>()
                .Select(p =>
                {
                    p.Load();
                    return p;
                }));
        }

        private void PrintVersionInfo()
        {
            _loggingService.Info(WorkbenchRuntime.RuntimeAssembly.GetName().Version);
        }
    }
}
