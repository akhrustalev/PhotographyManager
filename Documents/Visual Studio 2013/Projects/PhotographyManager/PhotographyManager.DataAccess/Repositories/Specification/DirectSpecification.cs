using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PhotographyManager.DataAccess.Repositories.Specification
{

    public sealed class DirectSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        #region Fields

        private readonly Expression<Func<TEntity, bool>> _matchingCriteria;

        #endregion

        #region Constructor

        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == null)
                throw new ArgumentNullException("matchingCriteria");

            _matchingCriteria = matchingCriteria;
        }

        #endregion

        #region Override

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _matchingCriteria;
        }

        #endregion
    }

}
