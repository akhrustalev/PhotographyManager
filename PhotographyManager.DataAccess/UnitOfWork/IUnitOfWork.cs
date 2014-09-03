using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using PhotographyManager.DataAccess.Repositories;
using PhotographyManager.Model;

namespace PhotographyManager.DataAccess.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        void Commit();

        IRepository<User> Users{get; }

        IRepository<Album> Albums { get; }

        IRepository<Photo> Photos{get; }

        IEnumerable<Photo> SearchPhotos(string keyword);

        IEnumerable<Photo> AdvancedSearchPhotos(string name, string shootingPlace, DateTime shootingTime, string cameraModel, string diaphragm, string ISO, double shutterSpeed, double focalDistance, bool flash);
        
    }
}
