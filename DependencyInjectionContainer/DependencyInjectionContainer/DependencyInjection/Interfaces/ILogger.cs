using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer.DependencyInjection
{
    public interface ILogger
    {
        void log(String str);

        int getID();
    }
}
