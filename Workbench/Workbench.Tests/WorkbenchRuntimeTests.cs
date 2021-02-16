using NUnit.Framework;
using System.Collections.Generic;
using Workbench.Infrastructure.DI;
using Workbench.Runtime;

namespace Workbench.Tests
{
    public class WorkbenchRuntimeTests
    {
        private List<IWorkbenchRuntime> _workbenchRuntimes = new List<IWorkbenchRuntime>();
        private IDependencyInjectionEngine _dependencyInjectionEngine = new NinjectEngine();
        [SetUp]
        public void Setup()
        {
            _workbenchRuntimes.Clear();
            _dependencyInjectionEngine.LoadAssembly(typeof(WorkbenchRuntimeTests).Assembly);
            _dependencyInjectionEngine.LoadAssembly(typeof(WorkbenchServerRuntime).Assembly);
            _dependencyInjectionEngine.LoadAssembly(typeof(WorkbenchClientRuntime).Assembly);
        }

        [Test]
        public void Test1()
        {
            _workbenchRuntimes.AddRange(_dependencyInjectionEngine.GetAll<IWorkbenchRuntime>());
            foreach(var runtime in _workbenchRuntimes)
            {
                runtime.Start();
            }
            Assert.IsTrue(_workbenchRuntimes.Count == 0);
        }
    }
}