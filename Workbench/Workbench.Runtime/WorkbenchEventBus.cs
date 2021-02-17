using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Events;
using Workbench.Infrastructure.Reflection;
using Workbench.Infrastructure.Services;

namespace Workbench.Runtime
{
    public class WorkbenchEventBus : IEventBus
    {
        private readonly ILoggingService _loggingService;
        private Dictionary<Type, List<Delegate>> _eventDispatchMap = new Dictionary<Type, List<Delegate>>();

        public WorkbenchEventBus(IDependencyInjectionEngine di) 
        {
            di.Inject(ref _loggingService);
        }

        private Type CreateEventHandlerDelegateType(MethodInfo methodInfo)
        {
            var typeArgs = methodInfo.GetParameters()
                .Select(pi => pi.ParameterType).ToList();
            typeArgs.Add(methodInfo.ReturnType);
            return Expression.GetDelegateType(typeArgs.ToArray());
        }
        public void RegisterListener(IEventListener listener)
        {
            var eventHandlerMethods = from mi in listener.GetType().GetMethods()
                                      where mi.GetCustomAttributes<EventHandlerAttribute>()
                                        .FirstOrDefault() != null && mi.GetParameters().Count() == 1
                                      select mi;

            foreach(var methodInfo in eventHandlerMethods)
            {
                var delegateType = CreateEventHandlerDelegateType(methodInfo);
                List<Delegate> eventDelegates = new List<Delegate>();
                Type eventParameterType = methodInfo.GetParameters()
                    .Select(pi => pi.ParameterType)
                    .FirstOrDefault();
                if(eventParameterType == null)
                {
                    throw new InvalidOperationException("Invalid event handler");
                }
                if(!_eventDispatchMap.TryGetValue(eventParameterType, out eventDelegates))
                {
                    eventDelegates = new List<Delegate>();
                    _eventDispatchMap.Add(eventParameterType, eventDelegates);
                }
                eventDelegates.Add(Delegate.CreateDelegate(delegateType, listener, methodInfo));
            }

        }

        public void PublishEvent<T>(T message)
        {
            if(_eventDispatchMap.TryGetValue(typeof(T), out List<Delegate> delegates))
            {
                foreach(var eventDelegate in delegates)
                {
                    try
                    {
                        eventDelegate.DynamicInvoke(message);
                    }
                    catch (Exception ex)
                    {
                        _loggingService.Error("Failed to invoke event", ex);
                    }
                }
            }
        }

        public void RegisterAssemblyHandlers(Assembly assembly)
        {
            //TODO: Use reflection to discover event handlers in a plugin assembly
        }
    }
}
