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
    public class WorkbenchRuntime : IDisposable
    {
        public static IDependencyInjectionEngine DependencyInjectionEngine { get; private set; }
        public static WorkbenchRuntime Runtime { get; private set; }
        public static Assembly RuntimeAssembly { get => typeof(WorkbenchRuntime).Assembly; }
        public static void Initialize()
        {
            DependencyInjectionEngine = new NinjectEngine();
            DependencyInjectionEngine.LoadAssembly(typeof(WorkbenchRuntime).Assembly);
            Runtime = new WorkbenchRuntime(DependencyInjectionEngine);
            Runtime.Run();
        }

        public static void Shutdown()
        {
            Runtime.Dispose();
        }

        public WorkbenchRuntimeConfig Config { get; private set; }

        private readonly ILoggingService _loggingSerivce;
        private readonly IDirectoryService _directoryService;

        private List<IWorkbenchPlugin> _loadedPlugins = new List<IWorkbenchPlugin>();

        public WorkbenchRuntime(IDependencyInjectionEngine di)
        {
            di.Inject(ref _loggingSerivce);
            di.Inject(ref _directoryService);

            Config = new WorkbenchRuntimeConfig(di);
        }
        
        private void PrintVersionInfo()
        {
            _loggingSerivce.Info(RuntimeAssembly.GetName().Version);
        }

        private void LoadRuntimeConfig()
        {
            RuntimeConfigurationAttribute assemblyConfigurationAttribute =
                RuntimeAssembly.GetCustomAttributes(typeof(RuntimeConfigurationAttribute), false).
                Cast<RuntimeConfigurationAttribute>().
                FirstOrDefault();
            if (assemblyConfigurationAttribute == null)
            {
                throw new InvalidOperationException("Must supply workbench assembly a configuration attribute");
            }
            Config.Load(assemblyConfigurationAttribute);
            _loggingSerivce.Debug(string.Format("Workbench directory: {0}, Workbench plugins directory: {0}",
                Config.WorkbenchDirectory, Config.WorkbenchPluginsDirectory));
        }

        private void LoadPlugins()
        {
            var pluginAssemblies = _directoryService.GetAllFilesInDirectory(Config.WorkbenchPluginsDirectory)
                .Where(fi => fi.Extension.Equals(".dll"))
                .Select(fi => Assembly.LoadFrom(fi.FullName))
                .Where(a => a.GetCustomAttribute(typeof(WorkbenchPluginAttribute)) != null)
                .ToList();

            foreach(var assembly in pluginAssemblies)
            {
                _loggingSerivce.Info(string.Format("{0} loaded.", assembly.FullName));
                DependencyInjectionEngine.GenerateAssemblyBindingsForType<IWorkbenchPlugin>(assembly);
            }
            _loadedPlugins.AddRange(DependencyInjectionEngine.GetAll<IWorkbenchPlugin>()
                .Select(p => 
                {
                    p.Load();
                    return p; 
                }));
        }

        public void Run()
        {
            _loggingSerivce.Info("Welcome to Workbench");
            PrintVersionInfo();
            _loggingSerivce.Info("Loading configuration...");
            LoadRuntimeConfig();
            _loggingSerivce.Info("Loading plugins...");
            LoadPlugins();
            _loggingSerivce.Info(string.Format("{0} plugins loaded!", _loadedPlugins.Count));
        }
        public void Dispose()
        {

        }
    }
}
