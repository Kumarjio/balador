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
                var message = await _test.SendMessage($"Id={messageItem.Id},Message={messageItem.Message}");
                return new ResponseMessage { Id = messageItem.Id, IsError = false, Message = message + " id=" + messageItem.Id + " ,Message=" + messageItem.Message };
            }
            catch (Exception e)
            {
                return new ResponseMessage { Id = messageItem.Id, IsError = true, ErrMessage = e.ToString(), Message = messageItem.Message };
            }
        }
    }
}
