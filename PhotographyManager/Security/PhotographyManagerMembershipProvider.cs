using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.UnitOfWork;

namespace PhotographyManager.Security
{
    //public class PhotographyManagerMembershipProvider
    //{
    //    IUnitOfWork _unitOfWork;
    //    public PhotographyManagerMembershipProvider(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }
    //    public UserProfile CreateUser(string username, string password)
    //    {
    //        UserProfile user = new UserProfile { UserName = username, Password = password };
    //        _unitOfWork.UserProfiles.Add(user);
    //        return user;
    //    }
    //    public bool ValidateUser(string userName, string password)
    //    {
    //        if (_unitOfWork.UserProfiles.GetAll().Where(user => user.UserName.Equals(userName)).FirstOrDefault().Password == password) return true;
    //        else return false;
    //    }
    //}
    
}