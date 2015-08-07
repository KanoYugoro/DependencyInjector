using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DependencyInjectionContainer.DependencyInjection
{
    public class MyControllerFactory : DefaultControllerFactory
    {

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            IController controller = (IController)DependencyInjector.getInstance().Resolve(controllerType);

            return controller;
        }
    }
}