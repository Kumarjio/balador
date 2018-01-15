using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.SimpleMessage
{
    [Export(typeof(IFactrorySendMessages))]
    [ExportMetadata("MessageType", "MockSender")]
    public class MockSender : IFactrorySendMessages
    {
        public async Task<ResponseMessages> GetSender(RegisterSender register)
        {
            ResponseMessages response = new ResponseMessages();
               SenderMessagesFactory sendFactory = new SenderMessagesFactory();
            var respndFactory = await sendFactory.ConnectAndLogin(register.Id, register.Pws);
            if (respndFactory.IsError)
            {
                response.IsError = true;
                response.Message = respndFactory.Message;
            }
            else
            {
                response.Result = respndFactory.Result;
            }
            return response;
        }
    }
}
