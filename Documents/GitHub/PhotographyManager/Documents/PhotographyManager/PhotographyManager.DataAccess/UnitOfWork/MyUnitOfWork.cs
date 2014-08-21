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
        private readonly PhotographyManagerContext context = new PhotographyManagerContext();

        public void Commit()
        {
            context.SaveChanges();
        }

        public Repository<FreeUser> GetUsers()
        {
            return new Repository<FreeUser>(context);
        }
        public Repository<Album> GetAlbums()
        {
            return new Repository<Album>(context);
        }

        public Repository<Photo> GetPhotos()
        {
            return new Repository<Photo>(context);
        }

    }
}
