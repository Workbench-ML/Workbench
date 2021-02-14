using log4net;
using System;

namespace Workbench.Infrastructure.Services
{
    public class LoggingService : ILoggingService
    {
        public static string OutputTitle => "Workbench";

        private readonly ILog logger;
        public LoggingService()
        {
            logger = LogManager.GetLogger(OutputTitle);
        }

        public void Debug(object message)
        {
            logger.Debug(message);
        }

        public void Error(object message, Exception ex = null)
        {
            logger.Error(message);
        }

        public void Info(object message)
        {
            logger.Info(message);
        }

        public void Warn(object message, Exception ex = null)
        {
            logger.Warn(message);
        }
    }
}
