using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench.Infrastructure.Services
{
    public interface ILoggingService 
    {
        void Info(object message);
        void Debug(object message);
        void Warn(object message, Exception ex = null);
        void Error(object message, Exception ex = null);
    }
}
