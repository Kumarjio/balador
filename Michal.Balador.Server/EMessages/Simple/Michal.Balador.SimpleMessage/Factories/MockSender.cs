using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.DataModel;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.SimpleMessage
{
    [Export(typeof(IFactrorySendMessages))]
    [ExportMetadata(ConstVariable.MESSAGE_TYPE, "MockSender")]
    [ExportMetadata(ConstVariable.DOMAIN_NAME, "com.baladorPlant")]
    public class MockSender : FactrorySendMessages
    {
        [ImportingConstructor()]
        public MockSender(IBaladorContext context) :base(context)
        {

        }

        public override AuthenticationManager GetAuthenticationManager()
        {
            return new HttpLiteAuthentication(Context, this);
        }

        protected override async Task<ResponseSenderMessages> GetSender(RegisterSender register)
        {
            ResponseSenderMessages response = new ResponseSenderMessages();
            try
            {
                var mockSend = new MockSend(Context,this);
                response =await mockSend.SetSocketClient(new SignUpSender { Id= register.Id},register.CanExcute);
                return response;
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
