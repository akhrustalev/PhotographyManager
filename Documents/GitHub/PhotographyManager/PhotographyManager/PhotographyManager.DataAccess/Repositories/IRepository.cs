using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace PhotographyManager.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
        void Add(TEntity item);
        void Remove(TEntity item);
        TEntity GetOne(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(int id);
        TEntity GetByName(Expression<Func<TEntity, bool>> filter);
        List<TEntity> GetAll();

    }
}
