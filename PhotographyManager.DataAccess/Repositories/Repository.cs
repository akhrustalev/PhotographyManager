using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using PhotographyManager.Model;
using System.Data.SqlClient;


namespace PhotographyManager.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        PhotographyManagerContext context;
        DbSet<TEntity> dbSet;



        public Repository(PhotographyManagerContext _context)
        {
            context = _context;
            dbSet = context.Set<TEntity>();
        }


        public virtual void Add(TEntity item)
        {
            dbSet.Add(item);
        }

        public virtual void Remove(TEntity item)
        {
            dbSet.Remove(item);
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> filter)
        {
           return dbSet.Where(filter).FirstOrDefault();

        }

        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Where(filter);
        }

        public virtual TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity GetByName(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Where(filter).FirstOrDefault();
        }


        public List<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public List<TEntity> Search(string keyword)
        {
            
            return context.Database.SqlQuery<TEntity>("SearchPhotos @KeyWord", new SqlParameter("KeyWord",keyword)).ToList();
        }

        public List<TEntity> AdvancedSearch(string name, string shootingPlace, DateTime shootingTime, string cameraModel, string diaphragm, string ISO, double shutterSpeed, double focalDistance, bool flash)
        {
            return context.Database.SqlQuery<TEntity>("AdvancedSearchPhoto @name, @shootingPlace, @shootingTime, @cameraModel, @diaphragm, @ISO, @shutterSpeed,@focalDistance,@flash", new SqlParameter("name", name), new SqlParameter("shootingPlace", shootingPlace), new SqlParameter("shootingTime", shootingTime), new SqlParameter("cameraModel", cameraModel), new SqlParameter("diaphragm", diaphragm), new SqlParameter("ISO", ISO), new SqlParameter("shutterSpeed", shutterSpeed), new SqlParameter("focalDistance", focalDistance), new SqlParameter("flash", flash)).ToList();

        }
        
    }

}
