using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Events;
using Workbench.Runtime;

namespace Workbench.Tests
{
    public class TestEvent
    {
        public int SecretKey { get; private set; }
        public TestEvent(int key)
        {
            SecretKey = key;
        }
    }

    public class WorkbenchRuntimeTests
    {
        private List<IWorkbenchRuntime> _workbenchRuntimes = new List<IWorkbenchRuntime>();
        private IDependencyInjectionEngine _dependencyInjectionEngine = new NinjectEngine();
        private WorkbenchEventBus _eventBus;
        [OneTimeSetUp]
        public void Setup()
        {
            _workbenchRuntimes.Clear();
            LoadTestAssembly(typeof(WorkbenchRuntimeTests).Assembly);
            LoadTestAssembly(typeof(WorkbenchServerRuntime).Assembly);
            LoadTestAssembly(typeof(WorkbenchClientRuntime).Assembly);
            _eventBus = _dependencyInjectionEngine.Get<IEventBus>() as WorkbenchEventBus;
            Assert.IsNotNull(_eventBus);
            _eventBus.RegisterListener(new TestEventListener());
            _eventBus.PublishEvent(new TestEvent(0x1337C0DE));
        }

        private void LoadTestAssembly(Assembly assembly)
        {
            _dependencyInjectionEngine.LoadAssembly(assembly);
        }

        [Test]
        public void TestRuntimeCreation()
        {
            _workbenchRuntimes.AddRange(_dependencyInjectionEngine.GetAll<IWorkbenchRuntime>());
            foreach (var runtime in _workbenchRuntimes)
            {
                runtime.Start();
            }
            Assert.IsTrue(_workbenchRuntimes.Count == 2);
        }
        #region Event Bus Tests
        private static readonly int TestEventKeyAnswer = 0x1337C0DE;
        private static int TestEventKey = 0;

        public class TestEventListener : IEventListener
        {
            [EventHandler]
            public void OnTestEvent(TestEvent testEvent)
            {
                TestEventKey = testEvent.SecretKey;
            }
        }


        [Test]
        public void TestEventDispatch()
        {
            Assert.IsTrue(TestEventKey == TestEventKeyAnswer);
        }
        #endregion
    }
}