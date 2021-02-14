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
        /// Gets or create an instance of Type T in the dependency injection engine
        /// </summary>
        /// <typeparam name="T">Requested instantiated type</typeparam>
        /// <returns>An instantiated instance of type T</returns>
        T Get<T>();
    }
}
