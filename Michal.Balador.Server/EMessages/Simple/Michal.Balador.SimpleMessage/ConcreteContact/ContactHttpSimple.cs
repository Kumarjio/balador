using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.SimpleMessage.ConcreteContact
{
    public class ContactHttpSimple : ContactManager
    {
        public ContactHttpSimple(AppMessanger appMessanger, ContactInfo contact) : base(appMessanger, contact)
        {
        }

       
        public override async Task<ResponseMessage> SendMessage(MessageItem messageItem)
        {
            var convert=(MockHttpSend)this.AppMessanger;
            var _test = convert.HttpClientTest;
            try
            {
                var message = await _test.SendMessage($"Id={messageItem.ClientId},Message={messageItem.Message}");
                return new ResponseMessage { ClientId = messageItem.ClientId, IsError = false, Message = message + " id=" + messageItem.ClientId + " ,Message=" + messageItem.Message };
            }
            catch (Exception e)
            {
                return new ResponseMessage { ClientId = messageItem.ClientId, IsError = true, ErrMessage = e.ToString(), Message = messageItem.Message };
            }
        }
    }
}
