using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PhotographyManager.DataAccess.UnitOfWork;
using Ninject;

namespace PhotographyManager.Web.Security
{
    public class WebSecurity
    {
        public static bool Login(string userName, string password, bool persistCookie = false)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                PhotographyManagerMembershipProvider provider = new PhotographyManagerMembershipProvider(unitOfWork);
                bool success = provider.ValidateUser(userName, password);
                if (success)
                {
                    FormsAuthentication.SetAuthCookie(userName, persistCookie);
                }
                return success;
            }
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public static void CreateUserAndAccount(string userName, string password, bool isPaid)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                PhotographyManagerMembershipProvider provider = new PhotographyManagerMembershipProvider(unitOfWork);
                provider.CreateUserAndAccount(userName, password, isPaid);
            }
        }

        public static bool IsUserInRole(string userName, string roleName)
        {
           using (UnitOfWork unitOfWork = new UnitOfWork())
           {
               PhotographyManagerRoleProvider provider = new PhotographyManagerRoleProvider(unitOfWork);
               return provider.IsUserInRole(userName, roleName);
           }
        }
    }
}