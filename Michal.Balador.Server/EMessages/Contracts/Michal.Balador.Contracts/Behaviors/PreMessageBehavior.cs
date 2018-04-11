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
   
    public abstract class PreMessageBehavior : Behavior
    {
        public PreMessageBehavior(IBaladorContext baladorContext) : base(baladorContext)
        {

        }

        public override async Task<ResponseBase> Excute<TRequestBehavior>(TRequestBehavior request) 
        {
            //MessageItem obj = (MessageItem)Convert.ChangeType(request, typeof(MessageItem));
            return null;
            //return await ChangeMessage(obj);
        }
       // public abstract Task<ResponseBase> ChangeMessage(MessageItem messageItem);
       
    }
}
