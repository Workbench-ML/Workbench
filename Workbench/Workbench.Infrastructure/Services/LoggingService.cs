using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench.Infrastructure.Services
{
    class LoggingService : ILoggingService
    {
        public LoggingService()
        {

        }
        public void Debug(object message)
        {
            throw new NotImplementedException();
        }

        public void Error(object message, Exception ex = null)
        {
            throw new NotImplementedException();
        }

        public void Info(object message)
        {
            throw new NotImplementedException();
        }

        public void Warn(object message, Exception ex = null)
        {
            throw new NotImplementedException();
        }
    }
}
