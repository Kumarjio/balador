using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Whatsup.ConcreteContact
{
    public class WhatsAppContactManager : ContactManager
    {
        public WhatsAppContactManager(AppMessanger appMessanger, ContactInfo contact) : base(appMessanger, contact)
        {
        }

        public override Task<ResponseMessage> SendMessage(MessageItem messageItem)
        {
            throw new NotImplementedException();
        }
    }
}
