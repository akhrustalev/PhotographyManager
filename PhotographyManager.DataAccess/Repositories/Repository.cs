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
        protected PhotographyManagerContext context;

        public Repository(PhotographyManagerContext _context)
        {
            context = _context;
        }

        public virtual void Add(TEntity item)
        {
            DbSet<TEntity> set = GetDbSet();
            set.Add(item);
        }

        public virtual void Remove(TEntity item)
        {
            DbSet<TEntity> set = GetDbSet();
            set.Remove(item);
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths)
        {
            DbSet<TEntity> set = GetDbSet();
            foreach (Expression<Func<TEntity, object>> path in paths)
                set.Include(path).ToList();
            return set.FirstOrDefault(filter);
        }

        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths)
        {
            DbSet<TEntity> set = GetDbSet();
            foreach (Expression<Func<TEntity, object>> path in paths)
                set.Include(path).ToList();
            return set.Where(filter);
        }

        public virtual TEntity GetById(int id, params Expression<Func<TEntity, object>>[] paths)
        {
            DbSet<TEntity> set = GetDbSet();
            foreach (Expression<Func<TEntity, object>> path in paths)
                set.Include(path).ToList();
            return set.First(e => e.ID == id);
        }
      
        private DbSet<TEntity> GetDbSet()
        {
            return context.Set<TEntity>();
        }


    }
}
