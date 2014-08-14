using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PhotographyManager.DataAccess.Repositories.Specification
{

    public sealed class TrueSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        #region Specification overrides

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            const bool result = true;

            Expression<Func<TEntity, bool>> trueExpression = t => result;
            return trueExpression;
        }

        #endregion
    }

}
