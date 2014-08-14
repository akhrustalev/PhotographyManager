using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.Model;

namespace PhotographyManager.DataAccess.Repositories.Users
{
    public interface IUserRepository
    {
        User GetById(int userId);
        User FindByUserName(string userName);
        void AddUser(User newUser);

        List<User> GetAll();
    }
}
