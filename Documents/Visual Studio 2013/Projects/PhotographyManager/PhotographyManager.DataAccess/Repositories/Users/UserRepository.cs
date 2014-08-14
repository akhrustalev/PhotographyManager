using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.Model;
using System.Data.Entity;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.DataAccess.Repositories.Specification;

namespace PhotographyManager.DataAccess.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbSet<User> Users;

        public UserRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Users = unitOfWork.GetDbSet<User>();
        }

        public User GetById(int userId)
        {
            return base.Get(userId);
        }

        public User FindByUserName(string userName)
        {
            return Users.FirstOrDefault(us => us.Name.Equals(userName));
        }

        public void AddUser(User newUser)
        {
            base.Add(newUser);
        }

        public List<User> GetAll()
        {
            return base.GetAll();
        }


    }
}
