using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Workbench.Infrastructure.Reflection
{
    public static class ReflectionUtils
    {
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            try
            {
                return assembly.GetTypes();
            }
            catch(ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
        public static IEnumerable<Type> GetTypesWithInterface<T>(this Assembly assembly)
        {
            return GetTypesWithInterface(assembly, typeof(T));
        }
        public static IEnumerable<Type> GetTypesWithInterface(this Assembly assembly, Type interfaceType)
        {
            return from type in assembly.GetLoadableTypes()
                   where interfaceType.IsAssignableFrom(type)
                   select type;
        }
    }
}
