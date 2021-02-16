using System;
using System.Collections.Generic;
using System.Text;

namespace Workbench.Infrastructure.DI
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class WorkbenchBindingAttribute : Attribute
    {

    }
}
