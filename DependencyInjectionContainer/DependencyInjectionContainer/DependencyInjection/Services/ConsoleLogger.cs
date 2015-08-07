using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DependencyInjectionContainer.DependencyInjection
{
    public class ConsoleLogger : ILogger
    {
        private static Random r = new Random();
        private int id;

        public ConsoleLogger()
        {
            id = r.Next(100);
        }

        public void log(string str)
        {
            Debug.WriteLine(str);
        }

        public int getID()
        {
            return id;
        }
    }
}