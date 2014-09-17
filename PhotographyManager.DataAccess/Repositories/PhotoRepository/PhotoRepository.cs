using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.Model;
using System.Data.SqlClient;

namespace PhotographyManager.DataAccess.Repositories.PhotoRepository
{
    public class PhotoRepository:Repository<Photo>,IPhotoRepository
    {
        public PhotoRepository(PhotographyManagerContext _context):base(_context)
        {
            context = _context;
            dbSet = context.Set<Photo>();
        }

        public IEnumerable<Photo> SearchPhotos(string keyword)
        {
            if (!(keyword.Equals(""))) return new List<Photo>();
            return context.Database.SqlQuery<Photo>("SearchPhotos @KeyWord", new SqlParameter("KeyWord", keyword)).ToList();
        }
        public IEnumerable<Photo> AdvancedSearchPhotos(string name, string shootingPlace, DateTime? shotAfter, DateTime? shotBefore, string cameraModel, string diaphragm, string ISO, double? shutterSpeed, double? focalDistance, bool? flash)
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
                shotBeforeParam = new SqlParameter("shotBefore", param);
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
            return context.Database.SqlQuery<Photo>("AdvancedSearchPhotos @name, @shootingPlace, @cameraModel, @ISO, @diaphragm, @shotAfter,@shotBefore,@shutterSpeed,@focalDistance,@flash", new SqlParameter("name", name), new SqlParameter("shootingPlace", shootingPlace), new SqlParameter("cameraModel", cameraModel), new SqlParameter("ISO", ISO),new SqlParameter("diaphragm",diaphragm), shotAfterParam, shotBeforeParam, shutterSpeedParam, focalDistanceParam, new SqlParameter("flash",flash)).ToList();
        }
    }
}
