using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.Model;
using System.Data.SqlClient;

namespace PhotographyManager.DataAccess.Repositories.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PhotographyManagerContext _context)
            : base(_context)
        {
            context = _context;
        }

        public void ChangeUsersTypeToPaid(int userId)
        {
            context.Database.SqlQuery<DBNull>("ChangeUsersTypeToPaid @ID",new SqlParameter("ID", userId)).ToList();
        }

        public void ChangeUsersTypeToFree(int userId)
        {
            context.Database.SqlQuery<DBNull>("ChangeUsersTypeToFree @ID", new SqlParameter("ID", userId)).ToList();
        }

    }
}
