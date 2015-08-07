using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyInjectionContainer.DependencyInjection
{
    public class DependencyInjector
    {
        private static DependencyResolver theResolver;
        
        public static DependencyResolver getInstance()
        {
            if (theResolver == null)
            {
                theResolver = new DependencyResolver();
            }

            return theResolver;
        }

    }
}