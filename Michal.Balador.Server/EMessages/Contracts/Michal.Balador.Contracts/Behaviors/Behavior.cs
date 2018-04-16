using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Contracts.Behaviors
{
    public enum BehaviorStatus {Continue=1,Pause=2 }
    public abstract class Behavior
    {
        protected IBaladorContext _baladorContext;
        //public Behavior(IBaladorContext baladorContext)
        //{
        //    _baladorContext = baladorContext;
        //}
        public IBaladorContext BaladorContext { get { return _baladorContext; } }

        public abstract Task<Response<BehaviorStatus>> Excute<TRequestBehavior>(TRequestBehavior request) where TRequestBehavior : RequestBehavior;
    }
    
}
