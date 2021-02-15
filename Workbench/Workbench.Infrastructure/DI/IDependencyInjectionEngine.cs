using System;
using System.Collections.Generic;
using System.Reflection;

namespace Workbench.Infrastructure.DI
{
    public interface IDependencyInjectionEngine
    {
        /// <summary>
        /// Load this assembly into the dependency injection engine
        /// </summary>
        /// <param name="assembly">Assembly to load</param>
        void LoadAssembly(Assembly assembly);

        /// <summary>
        /// Generates bindings for all the types in a given assembly that inherit from T
        /// </summary>
        /// <param name="assembly">Assembly to load</param>
        void GenerateAssemblyBindingsForType<T>(Assembly assembly);

        void GenerateAssemblyBindingsForType(Type type, Assembly assembly);
        /// <summary>
        /// Gets all instances that bind to type T
        /// </summary>
        /// <typeparam name="T">Binding type</typeparam>
        /// <returns>All instances associated with the multibinding of type T</returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// Gets or create an instance of Type T in the dependency injection engine
        /// </summary>
        /// <typeparam name="T">Requested instantiated type</typeparam>
        /// <returns>An instantiated instance of type T</returns>
        T Get<T>();

        /// <summary>
        /// Injects a reference of type T into the parameter
        /// </summary>
        /// <typeparam name="T">Type of service to inject</typeparam>
        void Inject<T>(ref T serviceRef);
    }
}
