using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotographyManager.Model;
using System.Web.Security;
using PhotographyManager.DataAccess.UnitOfWork;

namespace PhotographyManager.Web.Security
{
    public class PhotographyManagerRoleProvider
    {
       IUnitOfWork _unitOfWork;
        public PhotographyManagerRoleProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  bool IsUserInRole(string userName, string roleName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.Users.GetOne(u => u.Name == userName,u=>u.Roles);
                if (user == null)
                    return false;
                return user.Roles != null && user.Roles.Any(r => r.RoleName == roleName);
            }
        }
    }
}