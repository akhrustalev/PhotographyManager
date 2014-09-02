using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.DataAccess.Repositories;
using PhotographyManager.Model;

namespace PhotographyManager.DataAccess.UnitOfWork
{
    public class MyUnitOfWork : PhotographyManagerContext, IUnitOfWork
    {
        private  readonly PhotographyManagerContext context = new PhotographyManagerContext();

        public void Commit()
        {
            context.SaveChanges();
        }

        public IRepository<User> Users
        {
            get
            {
                return new Repository<User>(context);
            }
        }
        public IRepository<Album> Albums
        {
            get
            {
               return new Repository<Album>(context);
            }
 
        }

        public IRepository<Photo> Photos
        {
            get
            {
                return new Repository<Photo>(context);
            }
        }

        public void Dispose()
        {
            if (context != null) context.Dispose();
        }



    }
}
