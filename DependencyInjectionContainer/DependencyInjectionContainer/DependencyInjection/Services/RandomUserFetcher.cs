using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DependencyInjectionContainer.DependencyInjection;

namespace DependencyInjectionContainer.DependencyInjection
{
    public class RandomUserFetcher : IUserFetcher
    {
        INameService nameService;
        public RandomUserFetcher(INameService service)
        {
            nameService = service;
        }

        public Models.UserModel getUser()
        {
            Models.UserModel theUser = new Models.UserModel();

            theUser.username = nameService.getUsername(); ;
            theUser.email = "someusername@somedomain.com";
            theUser.firstname = nameService.getFirst();
            theUser.lastname = nameService.getLast();

            return theUser;
        }
    }
}