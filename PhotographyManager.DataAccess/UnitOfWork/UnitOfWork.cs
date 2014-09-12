using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.DataAccess.Repositories;
using PhotographyManager.Model;
using System.Data.SqlClient;
using System.Data.Entity.Validation;


namespace PhotographyManager.DataAccess.UnitOfWork
{
    public class UnitOfWork : PhotographyManagerContext, IUnitOfWork
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
        public IRepository<UserProfile> UserProfiles
        {
            get
            {
                return new Repository<UserProfile>(context);
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

        public IEnumerable<Photo> SearchPhotos(string keyword)
        {
            return context.Database.SqlQuery<Photo>("SearchPhotos @KeyWord", new SqlParameter("KeyWord", keyword)).ToList();
        }

        public IEnumerable<Photo> AdvancedSearchPhotos(string name, string shootingPlace, DateTime? shotAfter,DateTime? shotBefore, string cameraModel, string diaphragm, string ISO, double? shutterSpeed, double? focalDistance, bool? flash)
        {
            SqlParameter shotAfterParam = new SqlParameter();
            SqlParameter shotBeforeParam = new SqlParameter();
            SqlParameter shutterSpeedParam = new SqlParameter();
            SqlParameter focalDistanceParam = new SqlParameter();
            SqlParameter flashParam = new SqlParameter();
            if (shotAfter == null) 
            { 
                var param = DBNull.Value;
                shotAfterParam = new SqlParameter("shotAfter", param);
            }
            else 
            { 
                var param = shotAfter;
                shotAfterParam = new SqlParameter("shotAfter", param);
            }
            if (shotBefore == null) 
            { 
                var param = DBNull.Value;
                shotBeforeParam = new SqlParameter("shotBefore",param);
            }
            else 
            { 
                var param = shotBefore;
                shotBeforeParam = new SqlParameter("shotBefore", param);
            }
            if (shutterSpeed == null)
            {
                var param = DBNull.Value;
                shutterSpeedParam = new SqlParameter("shutterSpeed", param);
            }
            else
            {
                var param = shutterSpeed;
                shutterSpeedParam = new SqlParameter("shutterSpeed", param);
            }
            if (focalDistance == null)
            {
                var param = DBNull.Value;
                focalDistanceParam = new SqlParameter("focalDistance", param);
            }
            else
            {
                var param = shotBefore;
                focalDistanceParam = new SqlParameter("focalDistance", param);
            }
            if (flash == null)
            {
                var param = DBNull.Value;
                flashParam = new SqlParameter("flash", param);
            }
            else
            {
                var param = flash;
                flashParam = new SqlParameter("flash", param);
            }
            return context.Database.SqlQuery<Photo>("AdvancedSearchPhoto @name, @shootingPlace, @shotAfter,@shotBefore, @cameraModel, @diaphragm, @ISO, @shutterSpeed,@focalDistance,@flash", new SqlParameter("name", name), new SqlParameter("shootingPlace", shootingPlace), shotAfterParam,shotBeforeParam, new SqlParameter("cameraModel", cameraModel), new SqlParameter("diaphragm", diaphragm), new SqlParameter("ISO", ISO), shutterSpeedParam, focalDistanceParam, flashParam).ToList();
        }
    }
}
