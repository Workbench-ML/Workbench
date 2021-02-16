using System;
using System.Collections.Generic;
using System.Text;
using Workbench.Infrastructure.DI;
namespace Workbench.Runtime
{
    [WorkbenchBinding]
    public interface IWorkbenchRuntime : IDisposable
    {
        void Start();
        void LoadConfig();
        void LoadPlugins();
    }
}
