using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcYazilim.Dal.İnterfaces
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Rep { get; }
        bool Save();
    }
}
