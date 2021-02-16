using System;
using System.Collections.Generic;
using System.Linq;
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
            _injectionKernel.Load(new InfrastructureModule());
            _injectionKernel.Bind<IDependencyInjectionEngine>().ToConstant(this);
        }

        public void GenerateAssemblyBindingsForType<T>(Assembly assembly)
        {
            GenerateAssemblyBindingsForType(typeof(T), assembly);
        }

        public void GenerateAssemblyBindingsForType(Type type, Assembly assembly)
        {
            var assemblyBindings = assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && t.IsAssignableFrom(type))
                .ToList();
            foreach (var binding in assemblyBindings)
            {
                _injectionKernel.Bind(type).To(binding);
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _injectionKernel.GetAll<T>();
        }

        public T Get<T>() 
        {
            return _injectionKernel.Get<T>();
        }

        public void Inject<T>(ref T serviceRef)
        {
            serviceRef = _injectionKernel.Get<T>();
        }

        public void LoadAssembly(Assembly assembly)
        {
            _injectionKernel.Load(assembly);
        }
    }
}
