using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PhotographyManager.Model;


namespace PhotographyManager.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        void Add(TEntity item);
        void Remove(TEntity item);
        TEntity GetOne(Expression<Func<TEntity, bool>> filter,params Expression<Func<TEntity, object>>[] paths);
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] paths);
        TEntity GetById(int id, params Expression<Func<TEntity, object>>[] paths);
    }
}
