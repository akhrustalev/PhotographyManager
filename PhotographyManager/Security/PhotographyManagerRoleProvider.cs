using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotographyManager.Model;
using System.Web.Security;

namespace PhotographyManager.Security
{
    public class PhotographyManagerRoleProvider
    {
        public static bool IsUserInRole(string userName, string roleName)
        {
            using (var photographyManagerContext = new PhotographyManagerContext())
            {
                var user = photographyManagerContext.User.SingleOrDefault(u => u.Name == userName);
                if (user == null)
                    return false;
                return user.Roles != null && user.Roles.Any(r => r.RoleName == roleName);
            }
        }
    }
}