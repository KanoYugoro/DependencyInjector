using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DependencyInjectionContainer.DependencyInjection;
using DependencyInjectionContainer.Controllers;

namespace DependencyInjectionContainer
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Register the dependencies we need
            DependencyInjector.getInstance().Register<INameService, RandomNameService>();
            DependencyInjector.getInstance().Register<IUserFetcher, RandomUserFetcher>();
            DependencyInjector.getInstance().Register<ILogger, ConsoleLogger>(lifeStyleType.Singleton);
            DependencyInjector.getInstance().Register<HomeController, HomeController>();


            // Set the controller factory to our custom one
            ControllerBuilder.Current.SetControllerFactory(typeof(MyControllerFactory));
            
            

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}