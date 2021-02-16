using System.Reflection;
using Workbench.Infrastructure.DI;

namespace Workbench.Runtime
{
    public static class WorkbenchRuntime
    {
        public static IDependencyInjectionEngine DependencyInjectionEngine { get; private set; }
        public static IWorkbenchRuntime Runtime { get; private set; }
        public static Assembly RuntimeAssembly { get; set; }
        public static void Initialize()
        {
            DependencyInjectionEngine = new NinjectEngine();
            DependencyInjectionEngine.LoadAssembly(typeof(WorkbenchRuntime).Assembly);
            DependencyInjectionEngine.LoadAssembly(RuntimeAssembly);
        }

        public static void Start()
        {
            Runtime = DependencyInjectionEngine.Get<IWorkbenchRuntime>();
            Runtime.Start();
        }

        public static void Shutdown()
        {
            Runtime.Dispose();
        }
    }
}
