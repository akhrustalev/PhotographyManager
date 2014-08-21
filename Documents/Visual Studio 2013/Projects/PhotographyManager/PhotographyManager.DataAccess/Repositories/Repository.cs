using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PhotographyManager.DataAccess.UnitOfWork;


namespace PhotographyManager.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Members

        readonly IQueryableUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        public virtual void Add(TEntity item)
        {
            _unitOfWork.GetDbSet<TEntity>().Add(item);
        }

        //public virtual void Modify(TEntity item)
        //{
        //    _unitOfWork.SetEntryState(item, EntityState.Modified);
        //}

        public virtual void Remove(TEntity item)
        {
            _unitOfWork.SetEntryState(item, EntityState.Unchanged);
            _unitOfWork.GetDbSet<TEntity>().Remove(item);
        }

        public virtual TEntity Get(params object[] keyValues)
        {
            return _unitOfWork.GetDbSet<TEntity>().Find(keyValues);
        }



        public List<TEntity> GetAll()
        {
            return (List<TEntity>)_unitOfWork.GetDbSet<TEntity>().ToList();
        }
        
        #endregion


        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        #endregion

        #region "Private methods"



        #endregion
    }

}
