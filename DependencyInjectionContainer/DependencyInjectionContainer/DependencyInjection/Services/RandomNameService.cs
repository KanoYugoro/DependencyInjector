using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DependencyInjectionContainer.DependencyInjection;


namespace DependencyInjectionContainer.DependencyInjection
{
    public class RandomNameService : INameService
    {
        private static Random r = new Random();
        private string[] usernames;
        private string[] firsts;
        private string[] lasts;

        public RandomNameService()
        {
            usernames = new string[3] {"coolprogrammerman", "somedude15", "dependencyInjectionLover" };
            firsts = new string[3] { "Anson", "Matt", "Ryan" };
            lasts = new string[3] { "Wayman", "Smith", "Hauert" };
        }

        public string getUsername()
        {
            return usernames[(int)r.Next(usernames.Length)];
        }

        public string getFirst()
        {
            return firsts[(int)r.Next(firsts.Length)];
        }

        public string getLast()
        {
            return lasts[(int)r.Next(lasts.Length)];
        }
    }
}