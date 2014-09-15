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

        IRepository<UserMembership> UserMembership { get; }

        IRepository<UserRoles> UserRoles { get; }

        IRepository<Album> Albums { get; }

        IRepository<Photo> Photos{get; }

        IRepository<PhotoImage> PhotoImages { get; }

        IEnumerable<Photo> SearchPhotos(string keyword);

        IEnumerable<Photo> AdvancedSearchPhotos(string name, string shootingPlace, DateTime? shotAfter, DateTime? shotBefore, string cameraModel, string diaphragm, string ISO, double? shutterSpeed, double? focalDistance, bool? flash);
        
    }
}
