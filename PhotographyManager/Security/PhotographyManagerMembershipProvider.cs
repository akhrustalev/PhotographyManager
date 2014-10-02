using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.UnitOfWork;
using System.Web.Security;
using System.Web.Helpers;
using Ninject;

namespace PhotographyManager.Web.Security
{
    public class PhotographyManagerMembershipProvider
    {
        IUnitOfWork _unitOfWork;
        public PhotographyManagerMembershipProvider(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
       
        public void CreateUserAndAccount(string userName, string password)
        {
            User user = new FreeUser{Name=userName};
            if (_unitOfWork.UserRoles.GetOne(role => role.RoleName.Equals("User")) == null) 
            {
                _unitOfWork.UserRoles.Add(new UserRoles { RoleName = "User" }); 
                _unitOfWork.Commit();
            }
            user.Roles.Add(_unitOfWork.UserRoles.GetOne(role=>role.RoleName.Equals("User")));
            _unitOfWork.UserRoles.GetOne(role => role.RoleName.Equals("User"),u=>u.User).User.Add(user);
            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();
            int userId = _unitOfWork.Users.GetOne(u=>u.Name.Equals(userName)).ID;
            string salt = Crypto.GenerateSalt();
            string hashedPassword = Crypto.HashPassword(password + salt);
            _unitOfWork.UserMembership.Add(new UserMembership {ID = userId,Password = hashedPassword, PasswordSalt = salt});
            _unitOfWork.Commit();            
        }
        public bool ValidateUser(string userName, string password)
        {
            int userId = VerifyUserNameHasConfirmedAccount(userName);
            if (userId == -1) return false;
            else return CheckPassword(userId,password);            
        }

        private int VerifyUserNameHasConfirmedAccount(string UserName)
        {
            User user = _unitOfWork.Users.GetOne(u => u.Name.Equals(UserName));
            if (user == null) return -1;
            int userId = user.ID;
            if (_unitOfWork.UserMembership.GetOne(u => u.ID == userId)!= null) return userId;
            else return -1;
        }

        private bool CheckPassword(int userId, string password)
        {
            string hashedPassword = _unitOfWork.UserMembership.GetOne(user => user.ID == userId).Password;
            return (hashedPassword != null && Crypto.VerifyHashedPassword(hashedPassword, password+_unitOfWork.UserMembership.GetById(userId).PasswordSalt));
        }
    }    
}