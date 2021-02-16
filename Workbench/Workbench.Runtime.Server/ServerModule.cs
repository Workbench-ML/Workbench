using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workbench.Runtime.Server
{
    public class ServerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorkbenchRuntime>().To<WorkbenchServerRuntime>();
        }
    }
}
