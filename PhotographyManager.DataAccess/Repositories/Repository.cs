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
using System.Threading;


namespace PhotographyManager.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
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

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths)
        {
            foreach (Expression<Func<TEntity, object>> path in paths)
               dbSet.Include(path);
            try
            {
                return dbSet.FirstOrDefault(filter);
            }
            catch(System.Reflection.TargetException ex)
            {
                return null;
            }
        }

        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths)
        {
            foreach (Expression<Func<TEntity, object>> path in paths)
                dbSet.Include(path);
            return dbSet.Where(filter);
        }

        public virtual TEntity GetById(int id, params Expression<Func<TEntity, object>>[] paths)
        {
            foreach (Expression<Func<TEntity, object>> path in paths)
                dbSet.Include(path);
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet;
        }       
    }
}
