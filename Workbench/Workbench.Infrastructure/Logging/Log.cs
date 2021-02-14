using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench.Infrastructure.Logging
{
    public class Log
    {
        public static string OutputTitle => "Workbench";

        private static readonly ILog logger;

        static Log()
        {
            logger = LogManager.GetLogger(OutputTitle);
        }

        public static void Info(object message)
        {
            logger.Info(message);
        }
    }
}
