using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PhotographyManager.DataAccess.Repositories.Specification
{
    public sealed class NotSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        #region Fields

        private readonly Expression<Func<TEntity, bool>> _originalCriteria;

        #endregion

        #region Constructor

        public NotSpecification(ISpecification<TEntity> originalSpecification)
        {
            if (originalSpecification == null)
                throw new ArgumentNullException("originalSpecification");

            _originalCriteria = originalSpecification.SatisfiedBy();
        }

        public NotSpecification(Expression<Func<TEntity, bool>> originalSpecification)
        {
            if (originalSpecification == null)
                throw new ArgumentNullException("originalSpecification");

            _originalCriteria = originalSpecification;
        }

        #endregion

        #region Override Specification methods

        public override Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression.Not(_originalCriteria.Body), _originalCriteria.Parameters.Single());
        }

        #endregion
    }

}
