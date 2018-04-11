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
    public abstract class PostMessageBehavior: Behavior
    {
        public PostMessageBehavior(IBaladorContext baladorContext):base(baladorContext)
        {

        }

        public override Task<ResponseBase> Excute<TRequestBehavior>(TRequestBehavior request)
        {
            return Task.FromResult<ResponseBase>(new ResponseBase { });
        }
    }
}
