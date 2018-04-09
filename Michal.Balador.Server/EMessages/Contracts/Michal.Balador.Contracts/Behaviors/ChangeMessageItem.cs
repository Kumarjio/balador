using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Behaviors
{
    public class ChangeMessageItemBehavior: PreMessageBehavior
    {
        public ChangeMessageItemBehavior(IBaladorContext baladorContext) : base(baladorContext)
        {

        }
        public override Task<ResponseBase> ChangeMessage(MessageItem messageItem)
        {
            return Task.FromResult<ResponseBase>(new ResponseBase { });
        }
    }
}
