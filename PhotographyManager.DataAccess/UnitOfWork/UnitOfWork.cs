using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.DataAccess.Repositories;
using PhotographyManager.Model;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using PhotographyManager.DataAccess.Repositories.PhotoRepository;
using PhotographyManager.DataAccess.Repositories.UserRepository;


namespace PhotographyManager.DataAccess.UnitOfWork
{
    public class UnitOfWork : PhotographyManagerContext, IUnitOfWork
    {
        private  readonly PhotographyManagerContext context = new PhotographyManagerContext();

        public void Commit()
        {
            context.SaveChanges();
        }

        public IUserRepository Users
        {
            get
            {
                return new UserRepository(context);
            }
        }
        public IRepository<UserMembership> UserMembership
        {
            get
            {
                return new Repository<UserMembership>(context);
            }
        }

        public IRepository<UserRoles> UserRoles
        {
            get
            {
                return new Repository<UserRoles>(context);
            }
        }

        public IRepository<Album> Albums
        {
            get
            {
               return new Repository<Album>(context);
            }
 
        }

        public IPhotoRepository Photos
        {
            get
            {
                return new PhotoRepository(context);
            }
        }

        public IRepository<PhotoImage> PhotoImages
        {
            get
            {
                return new Repository<PhotoImage>(context);
            }
        }

        public void Dispose()
        {
            if (context != null) context.Dispose();
        }
    }
}
