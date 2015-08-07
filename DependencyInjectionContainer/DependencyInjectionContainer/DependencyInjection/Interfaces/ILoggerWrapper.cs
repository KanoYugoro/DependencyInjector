using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer.DependencyInjection
{
    public interface ILoggerWrapper
    {

        void log(string str);
        ILogger getWrappedLogger();
    }
}
