using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Behaviors
{
   
    public abstract class PreMessageBehavior : Behavior
    {
        public PreMessageBehavior(IBaladorContext baladorContext) : base(baladorContext)
        {

        }

        public override async Task<ResponseBase> Excute<TRequest>(TRequest request)
        {
            MessageItem obj = (MessageItem)Convert.ChangeType(request, typeof(MessageItem));

            return await ChangeMessage(obj);
        }
        public abstract Task<ResponseBase> ChangeMessage(MessageItem messageItem);
       
    }
}
