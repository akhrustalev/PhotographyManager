using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace PhotographyManager.Model
{
    public class UsersContext:DbContext
    {
        public UsersContext()
            : base("PhotographyManagerContext")
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
