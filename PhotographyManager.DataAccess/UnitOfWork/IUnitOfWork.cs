using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using PhotographyManager.DataAccess.Repositories;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.Repositories.PhotoRepository;
using PhotographyManager.DataAccess.Repositories.UserRepository;

namespace PhotographyManager.DataAccess.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        void Commit();

        IUserRepository Users{get; }

        IRepository<UserMembership> UserMembership { get; }

        IRepository<UserRoles> UserRoles { get; }

        IRepository<Album> Albums { get; }

        IPhotoRepository Photos{get; }

        IRepository<PhotoImage> PhotoImages { get; }

        IRepository<Comment> Comments { get; }
        
    }
}
