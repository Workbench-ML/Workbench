using System;
using System.Collections.Generic;
using System.Text;
using Workbench.Runtime;

namespace Workbench.Runtime.Server
{
    internal class ServerRunner
    {
        public static void Main()
        {
            WorkbenchRuntime.RuntimeAssembly = typeof(WorkbenchServerRuntime).Assembly;
            WorkbenchRuntime.Initialize();
            WorkbenchRuntime.Start();
        }
    }
}
