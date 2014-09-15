using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PhotographyManager.DataAccess.UnitOfWork;
using Ninject;

namespace PhotographyManager.Security
{
    public class WebSecurity
    {
        static PhotographyManagerMembershipProvider provider=new PhotographyManagerMembershipProvider(new UnitOfWork());
        public static bool Login(string userName, string password, bool persistCookie = false)
        {
            bool success = provider.ValidateUser(userName, password);
            if (success)
            {
                FormsAuthentication.SetAuthCookie(userName, persistCookie);
            }
            return success;
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public static void CreateUserAndAccount(string userName, string password)
        {
            provider.CreateUserAndAccount(userName, password);
        }
    }
}