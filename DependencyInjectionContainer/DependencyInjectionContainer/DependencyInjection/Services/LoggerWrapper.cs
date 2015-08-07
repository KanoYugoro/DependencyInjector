using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyInjectionContainer.DependencyInjection
{
    public class LoggerWrapper : ILoggerWrapper
    {
        private ILogger wrappedLogger;

        public LoggerWrapper(ILogger logger)
        {
            wrappedLogger = logger;
        }

        public void log(string str)
        {
            wrappedLogger.log(str);
        }

        public ILogger getWrappedLogger()
        {
            return wrappedLogger;
        }
    }
}