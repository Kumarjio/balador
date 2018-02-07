using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.SimpleMessage
{
    [Export(typeof(IFactrorySendMessages))]
    [ExportMetadata("MessageType", "MockSender")]
    public class MockSender : FactrorySendMessages
    {
        [ImportingConstructor()]
        public MockSender(IBaladorContext context) :base(context)
        {

        }
        public override async Task<ResponseSenderMessages> GetSender(RegisterSender register)
        {
            ResponseSenderMessages response = new ResponseSenderMessages();
            try
            {
                SenderMessagesFactory sendFactory = new SenderMessagesFactory(this.Context);
                var respndFactory = await sendFactory.ConnectAndLogin(register.Id, register.Pws);
                if (respndFactory.IsError)
                {
                    response.IsError = true;
                    response.Message = respndFactory.Message;
                }
                else
                    response.Result = respndFactory.Result;
            }
            catch (Exception e)
            {

                response.IsError = true;
                response.Message = e.Message;
            }
        
            
            return response;
        }

       
    }
}
