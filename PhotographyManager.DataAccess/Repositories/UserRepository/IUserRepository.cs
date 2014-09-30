using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.Model;

namespace PhotographyManager.DataAccess.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void ChangeUsersTypeToPaid(int userId);
        void ChangeUsersTypeToFree(int userId);

    }
}
