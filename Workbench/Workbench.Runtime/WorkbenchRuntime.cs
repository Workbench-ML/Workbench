using System;
using System.Linq;
using System.Reflection;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Services;
using Workbench.Runtime.Config;

namespace Workbench.Runtime
{
    public class WorkbenchRuntime : IDisposable
    {
        public static IDependencyInjectionEngine DependencyInjectionEngine { get; private set; }
        public static WorkbenchRuntime Runtime { get; private set; }
        public static Assembly RuntimeAssebly { get => typeof(WorkbenchRuntime).Assembly; }
        public static void Initialize()
        {
            DependencyInjectionEngine = new NinjectEngine();
            DependencyInjectionEngine.LoadAssembly(typeof(WorkbenchRuntime).Assembly);
            Runtime = DependencyInjectionEngine.Get<WorkbenchRuntime>();
        }

        public static void Shutdown()
        {
            Runtime.Dispose();
        }

        public WorkbenchRuntimeConfig Config { get; private set; }

        private readonly ILoggingService _loggingSerivce;

        public WorkbenchRuntime(IDependencyInjectionEngine di)
        {
            _loggingSerivce = di.Get<ILoggingService>();
            Config = di.Get<WorkbenchRuntimeConfig>();
        }
        
        private void PrintVersionInfo()
        {
            _loggingSerivce.Info(RuntimeAssebly.GetName().Version);
        }

        private void LoadRuntimeConfig()
        {
            RuntimeConfigurationAttribute assemblyConfigurationAttribute =
                RuntimeAssebly.GetCustomAttributes(typeof(RuntimeConfigurationAttribute), false).
                Cast<RuntimeConfigurationAttribute>().
                FirstOrDefault();
            if(assemblyConfigurationAttribute == null)
            {
                throw new InvalidOperationException("Must supply workbench assembly a configuration attribute");
            }
            Config.Load(assemblyConfigurationAttribute);
        }

        public void Run()
        {
            _loggingSerivce.Info("Welcome to Workbench");
            PrintVersionInfo();
            _loggingSerivce.Info("Loading configuration...");
            LoadRuntimeConfig();
        }
        public void Dispose()
        {

        }
    }
}
