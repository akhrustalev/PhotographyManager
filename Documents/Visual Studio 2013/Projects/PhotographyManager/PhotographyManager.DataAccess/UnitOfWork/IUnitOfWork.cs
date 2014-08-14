using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyManager.DataAccess.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
        void DiscardChanges();
    }
}
