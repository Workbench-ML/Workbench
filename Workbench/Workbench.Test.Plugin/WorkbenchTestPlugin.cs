using System;
using System.Collections.Generic;
using System.Text;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Services;
using Workbench.Runtime.Plugin;

namespace Workbench.Test.Plugin
{
    public class WorkbenchTestPlugin : IWorkbenchPlugin
    {
        private readonly ILoggingService _loggingService;

        public WorkbenchTestPlugin(IDependencyInjectionEngine di)
        {
            di.Inject(ref _loggingService);
        }
        public void Load()
        {
            _loggingService.Info("Workbench Test Plugin Loaded");
        }
    }
}
