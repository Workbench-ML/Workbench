using System;

namespace Workbench.Infrastructure.Events
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EventHandlerAttribute : Attribute
    {

    }
}
