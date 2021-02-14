using System;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Services;

namespace Workbench.Runtime
{
    public class WorkbenchRuntime : IDisposable
    {
        public static IDependencyInjectionEngine DependencyInjectionEngine { get; private set; }
        public static WorkbenchRuntime Runtime { get; private set; }

        public static void Initialize()
        {
            DependencyInjectionEngine = new NinjectEngine();
            DependencyInjectionEngine.LoadAssembly(typeof(WorkbenchRuntime).Assembly);
            Runtime = DependencyInjectionEngine.Get<WorkbenchRuntime>();
        }

        public WorkbenchRuntime(ILoggingService loggingService)
        {
            loggingService.Info("Hello world");
        }

        public void Dispose()
        {

        }
    }
}
