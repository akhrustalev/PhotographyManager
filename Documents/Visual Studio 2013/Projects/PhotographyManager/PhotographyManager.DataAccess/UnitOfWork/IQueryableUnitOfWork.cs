using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace PhotographyManager.DataAccess.UnitOfWork
{
    public interface IQueryableUnitOfWork: IUnitOfWork
    {
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
        void SetEntryState<TEntity>(TEntity item, EntityState state) where TEntity : class;
        IQueryable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);
        int ExecuteCommand(string sqlCommand, params object[] parameters);

    }
}
