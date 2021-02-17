using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workbench.Infrastructure.Events;
using Workbench.Runtime;

namespace Workbench.Tests
{
    public class WorkbenchTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEventBus>().To<WorkbenchEventBus>();
        }
    }
}
