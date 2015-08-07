using DependencyInjectionContainer.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DependencyInjectionContainer.Models;

namespace DependencyInjectionContainer.Controllers
{
    public class HomeController : Controller
    {
        ILogger myLogger;
        IUserFetcher myUserFetcher;
        //
        // GET: /Home/
        public HomeController(ILogger theLogger, IUserFetcher userFetcher)
        {
            myLogger = theLogger;
            myUserFetcher = userFetcher;
        }

        public ActionResult Index()
        {
            myLogger.log("Hey, we got an index request.");

            UserModel userModel = myUserFetcher.getUser();

            return View(model: userModel);
        }

    }
}
