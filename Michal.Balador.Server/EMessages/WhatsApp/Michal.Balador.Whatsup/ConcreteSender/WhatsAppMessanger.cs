using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.WhatsApp.ConcreteContact;
using Michal.Balador.WhatsApp.Config;
using WhatsAppApi;

namespace Michal.Balador.WhatsApp.ConcreteSender
{
    public class WhatsAppMessanger : AppMessanger
    {
      
        public WhatsAppMessanger(IBaladorContext context, AppMessangerFactrory provider) : base(context, provider)
        {

     
        }

        protected virtual ContactManager GetInstanceContactManger(ContactInfo contact)
        {
            return new WhatsAppContactManager(this, contact);
        }
        public override void Dispose()
        {
            
        }

        
        public override async Task<ResponseSend> Send(SendRequest request)
        {
            // wa = new WhatsApp(request.
            return await Task.FromResult(new ResponseSend());
        }
    }
}
