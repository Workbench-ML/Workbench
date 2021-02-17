using System;
using System.Collections.Generic;
using System.Text;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Events;

namespace Workbench.Runtime
{
    public interface IWorkbenchRuntime : IDisposable
    {
        void Start();
        void RegisterEventListener(IEventListener eventListener);
    }
}
