using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
    public class MockSender : IFactrorySendMessages
    {
        public Task<ResponseMessages> GetSender(RegisterSender register)
        {
            MockSend send = new MockSend();
               ResponseMessages messages = new ResponseMessages();
            messages.Result = send;
            //will be conent
            return Task.FromResult(messages);
        }
    }
}
