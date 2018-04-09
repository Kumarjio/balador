using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Behaviors
{
    public abstract class PostMessageBehavior: Behavior
    {
        public PostMessageBehavior(IBaladorContext baladorContext):base(baladorContext)
        {

        }

        public override Task<ResponseBase> Excute<TRequest>(TRequest request)
        {
            return Task.FromResult<ResponseBase>(new ResponseBase { });
        }
    }
}
