using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyManager.DataAccess.Repositories.Specification
{
    public abstract class CompositeSpecification<TEntity>: Specification<TEntity> where TEntity: class
    {
        #region Properties

        public abstract ISpecification<TEntity> LeftSideSpecification { get; }
        public abstract ISpecification<TEntity> RightSideSpecification { get; }

        #endregion

    }
}
