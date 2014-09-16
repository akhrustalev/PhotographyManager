using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.Model;

namespace PhotographyManager.DataAccess.Repositories.PhotoRepository
{
    public interface IPhotoRepository:IRepository<Photo>
    {
        IEnumerable<Photo> SearchPhotos(string keyword);
        IEnumerable<Photo> AdvancedSearchPhotos(string name, string shootingPlace, DateTime? shotAfter, DateTime? shotBefore, string cameraModel, string diaphragm, string ISO, double? shutterSpeed, double? focalDistance, bool? flash);
    }
}
