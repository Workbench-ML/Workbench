using System.Reflection;

using Ninject;

namespace Workbench.Infrastructure.DI
{
    public class NinjectEngine : IDependencyInjectionEngine
    {
        private IKernel _injectionKernel;

        public NinjectEngine()
        {
            _injectionKernel = new StandardKernel();
            _injectionKernel.Bind<IDependencyInjectionEngine>().ToSelf();
            _injectionKernel.Load(new InfrastructureModule());
        }

        public T Get<T>() 
        {
            return _injectionKernel.Get<T>();
        }

        public void LoadAssembly(Assembly assembly)
        {
            _injectionKernel.Load(assembly);
        }
    }
}
