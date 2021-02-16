using System;
using System.Collections.Generic;
using System.Text;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Services;

namespace Workbench.Runtime
{
    public class WorkbenchServerRuntime : BaseWorkbenchRuntime
    {
        public WorkbenchServerRuntime(IDependencyInjectionEngine di)
            : base(di)
        {

        }
        public override void Start()
        {
            _loggingService.Info("Welcome to Workbench - Server Runtime");
        }
    }
}
