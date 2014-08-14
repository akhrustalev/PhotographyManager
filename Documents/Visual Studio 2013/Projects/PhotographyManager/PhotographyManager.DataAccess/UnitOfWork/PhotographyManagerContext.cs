using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using PhotographyManager.Model;
namespace PhotographyManager.DataAccess.UnitOfWork
{
    public class PhotographyManagerContext : PhotographyManagerModel,IQueryableUnitOfWork
    {
        #region Constructor

        private readonly ObjectContext _objectContext;

        public PhotographyManagerContext()
        {
            Configuration.LazyLoadingEnabled = true;
            _objectContext = (this as IObjectContextAdapter).ObjectContext;
        }
    
        #endregion

        #region "IUnitOfWork Members"

        public void Commit()
        {
            SaveChanges();
        }

        public void DiscardChanges()
        {
            // code from: http://code.msdn.microsoft.com/How-to-undo-the-changes-in-00aed3c4#content
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from  
                    // Modified to Unchanged first sets the values of all  
                    // properties to the original values that were read from  
                    // the database when it was queried, and then marks the  
                    // entity as Unchanged. This will also reject changes to  
                    // FK relationships since the original value of the FK  
                    // will be restored. 
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }

        }

        #endregion

        #region "IQueryableUnitOfWork Members"

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public void SetEntryState<TEntity>(TEntity item, EntityState state) where TEntity : class
        {
            Entry(item).State = state;
        }

        public IQueryable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<TEntity>(sqlQuery, parameters).AsQueryable();
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

    }
}
