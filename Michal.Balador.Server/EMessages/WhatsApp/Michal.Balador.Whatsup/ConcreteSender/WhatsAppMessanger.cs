using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Whatsup.ConcreteSender
{
    public class WhatsAppMessanger : AppMessanger
    {
        public WhatsAppMessanger(IBaladorContext context, AppMessangerFactrory provider) : base(context, provider)
        {
        }

        public override void Dispose()
        {
            
        }

        public override async Task<ResponseSend> Send(SendRequest request)
        {
            return await Task.FromResult(new ResponseSend());
        }
    }
}
