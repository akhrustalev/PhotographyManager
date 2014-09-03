using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.DataAccess.Repositories;
using PhotographyManager.Model;
using System.Data.SqlClient;


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

        public IEnumerable<Photo> SearchPhotos(string keyword)
        {

            return context.Database.SqlQuery<Photo>("SearchPhotos @KeyWord", new SqlParameter("KeyWord", keyword)).ToList();
        }

        public IEnumerable<Photo> AdvancedSearchPhotos(string name, string shootingPlace, DateTime shootingTime, string cameraModel, string diaphragm, string ISO, double shutterSpeed, double focalDistance, bool flash)
        {
            return context.Database.SqlQuery<Photo>("AdvancedSearchPhoto @name, @shootingPlace, @shootingTime, @cameraModel, @diaphragm, @ISO, @shutterSpeed,@focalDistance,@flash", new SqlParameter("name", name), new SqlParameter("shootingPlace", shootingPlace), new SqlParameter("shootingTime", shootingTime), new SqlParameter("cameraModel", cameraModel), new SqlParameter("diaphragm", diaphragm), new SqlParameter("ISO", ISO), new SqlParameter("shutterSpeed", shutterSpeed), new SqlParameter("focalDistance", focalDistance), new SqlParameter("flash", flash)).ToList();

        }



    }
}
