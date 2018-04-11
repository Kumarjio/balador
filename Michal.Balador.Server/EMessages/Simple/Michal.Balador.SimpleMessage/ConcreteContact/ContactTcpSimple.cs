using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.SimpleMessage.ConcreteContact
{
    public class ContactTcpSimple : ContactManager
    {
        public ContactTcpSimple(AppMessanger appMessanger, ContactInfo contact) : base(appMessanger, contact)
        {
        }

        public override Task<ResponseBase> Init()
        {
            throw new NotImplementedException();
        }

        public override Task<ResponseMessage> SendMessage(MessageItem messageItem)
        {
            throw new NotImplementedException();
        }
    }
}
