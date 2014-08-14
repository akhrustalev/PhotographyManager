using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PhotographyManager.DataAccess.Repositories.Specification
{
    public interface ISpecification<TEntity> where TEntity: class
    {
        Expression<Func<TEntity, bool>> SatisfiedBy();

        List<string> Includes { get; set; }
    }
}
