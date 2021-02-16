using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;


namespace Workbench.Runtime.Client
{
    public class ClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorkbenchRuntime>().To<WorkbenchClientRuntime>();
        }
    }
}
