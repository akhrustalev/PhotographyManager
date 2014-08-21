using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotographyManager.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
        void Add(TEntity item);
        //void Modify(TEntity item);
        void Remove(TEntity item);
        TEntity Get(params object[] keyValues);



        List<TEntity> GetAll();

    }
}
