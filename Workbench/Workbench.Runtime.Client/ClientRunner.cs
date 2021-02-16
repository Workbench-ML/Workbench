using System;
using System.Collections.Generic;
using System.Text;
using Workbench.Runtime;
namespace Workbench.Runtime.Client
{
    internal class ClientRunner
    {
        public static void Main()
        {
            WorkbenchRuntime.RuntimeAssembly = typeof(WorkbenchClientRuntime).Assembly;
            WorkbenchRuntime.Initialize();
            WorkbenchRuntime.Start();
        }
    }
}
