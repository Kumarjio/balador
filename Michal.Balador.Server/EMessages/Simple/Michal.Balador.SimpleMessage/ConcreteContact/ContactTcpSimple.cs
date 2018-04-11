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

        public override async Task<ResponseMessage> SendMessage(MessageItem messageItem)
        {
            var convert = (MockTcpSend)this.AppMessanger;
            var _test = convert.SocketClientTest;
            await Task.Run(() =>
            {
                try
                {
                    var message =  _test.SendMessage(messageItem.Id, messageItem.Message);
                    return new ResponseMessage { Id = messageItem.Id, IsError = false, Message = message + " id=" + messageItem.Id + " ,Message=" + messageItem.Message };
                }
                catch (Exception e)
                {
                    return new ResponseMessage { Id = messageItem.Id, IsError = true, ErrMessage = e.ToString(), Message = messageItem.Message };
                }
            });
            return new ResponseMessage { Id = messageItem.Id, IsError = true, ErrMessage = "", Message = messageItem.Message };

        }
    }
}
