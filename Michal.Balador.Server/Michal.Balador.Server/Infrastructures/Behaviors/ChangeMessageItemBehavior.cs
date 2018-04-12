using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Server.Infrastructures.Behaviors
{
    public class ChangeMessageItemBehavior: PreMessageBehavior
    {
        //public ChangeMessageItemBehavior(IBaladorContext baladorContext) : base(baladorContext)
        //{

        //}

        public override async Task<ResponseBase> PreSend(RequestPreMessageBehavior requestMessageBehavior)
        {

            requestMessageBehavior.BaladorContext.GetLogger().Log( System.Diagnostics.TraceLevel.Info, $"test presend {requestMessageBehavior.Message}");

            return await Task.FromResult(new ResponseBase());
        }
      
    }
}
