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
        
        public override async Task<ResponseMessage> SendMessage(MessageItem messageItem)
        {
            var convert = (MockTcpSend)this.AppMessanger;
            var _test = convert.SocketClientTest;
            await Task.Run(() =>
            {
                try
                {
                    var message =  _test.SendMessage(messageItem.ClientId, messageItem.Message);
                    return new ResponseMessage { ClientId = messageItem.ClientId, IsError = false, Message = message + " id=" + messageItem.ClientId + " ,Message=" + messageItem.Message };
                }
                catch (Exception e)
                {
                    return new ResponseMessage { ClientId = messageItem.ClientId, IsError = true, ErrMessage = e.ToString(), Message = messageItem.Message };
                }
            });
            return new ResponseMessage { ClientId = messageItem.ClientId, IsError = true, ErrMessage = "", Message = messageItem.Message };

        }
    }
}
