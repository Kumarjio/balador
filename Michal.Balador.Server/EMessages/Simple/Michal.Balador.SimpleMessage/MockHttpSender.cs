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
    [ExportMetadata("MessageType", "MockHttpSender")]
    public class MockHttpSender : IFactrorySendMessages
    {
        public async Task<ResponseSenderMessages> GetSender(RegisterSender register)
        {
            ResponseSenderMessages response = new ResponseSenderMessages();
            try
            {
                response.Result = new MockHttpSend();
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Message = e.Message;
            }
            return await  Task.FromResult<ResponseSenderMessages>( response);
        }
    }
}
