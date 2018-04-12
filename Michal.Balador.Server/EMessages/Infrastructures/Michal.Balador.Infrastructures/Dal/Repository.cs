using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Dal;

namespace Michal.Balador.Infrastructures.Dal
{
    public  class Repository: IRepository
    {
        protected IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IUnitOfWork DbContext { get { return _unitOfWork; } }

        public virtual void Dispose()
        {
           _unitOfWork.Dispose();
        }
    }
}
