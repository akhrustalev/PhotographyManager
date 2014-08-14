using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PhotographyManager.DataAccess.Repositories.Specification.ExpressionTreeSerialization;

namespace PhotographyManager.DataAccess.Repositories.Specification
{

    public sealed class OrSpecification<T> : CompositeSpecification<T> where T : class
    {
        #region Fields

        private readonly ISpecification<T> _leftSideSpecification;
        private readonly ISpecification<T> _rightSideSpecification;

        #endregion

        #region Public Constructor

        public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            if (leftSide == null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == null)
                throw new ArgumentNullException("rightSide");

            _leftSideSpecification = leftSide;
            _rightSideSpecification = rightSide;
        }

        #endregion

        #region Composite Specification overrides

        public override ISpecification<T> LeftSideSpecification
        {
            get { return _leftSideSpecification; }
        }

        public override ISpecification<T> RightSideSpecification
        {
            get { return _rightSideSpecification; }
        }

        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            var left = _leftSideSpecification.SatisfiedBy();
            var right = _rightSideSpecification.SatisfiedBy();

            return (left.Or(right));
        }

        #endregion
    }

}
