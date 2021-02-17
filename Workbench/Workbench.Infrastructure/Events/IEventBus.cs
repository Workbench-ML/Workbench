using System;
using System.Collections.Generic;
using System.Text;

namespace Workbench.Infrastructure.Events
{
    public interface IEventBus
    {
        void RegisterListener(IEventListener eventListner);
        void PublishEvent<T>(T message);
        
    }
}
