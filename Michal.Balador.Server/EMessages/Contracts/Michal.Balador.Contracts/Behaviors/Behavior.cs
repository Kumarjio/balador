using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Behaviors
{
   
    public abstract class Behavior
    {
        protected IBaladorContext _baladorContext;
        public Behavior(IBaladorContext baladorContext)
        {
            _baladorContext = baladorContext;
        }
        public IBaladorContext BaladorContext { get { return _baladorContext; } }

        public abstract Task<ResponseBase> Excute<TRequest>(TRequest request) where TRequest : RequestBehavior;
    }
    
}
