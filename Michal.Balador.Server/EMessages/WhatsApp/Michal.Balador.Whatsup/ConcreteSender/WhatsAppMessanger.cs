using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.NSWhatsApp.ConcreteContact;
using Michal.Balador.NSWhatsApp.Config;
using WhatsAppApi;

namespace Michal.Balador.NSWhatsApp.ConcreteSender
{
    public class WhatsAppMessanger : AppMessanger
    {
        WhatsApp _wa;
        public WhatsApp WhatsApp { get { return _wa; } }
        public WhatsAppMessanger(IBaladorContext context, AppMessangerFactrory provider, WhatsApp wa) : base(context, provider)
        {
            _wa = wa;
        }

        protected override ContactManager GetInstanceContactManger(ContactInfo contact)
        {
            return new WhatsAppContactManager(this, contact, _wa);
        }
        public override void Dispose()
        {
            _wa.Disconnect();
        }

        
    }
}
