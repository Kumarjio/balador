using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Dal
{
    public interface IRepository : IDisposable
    {
        IUnitOfWork DbContext { get; }
    }
}
