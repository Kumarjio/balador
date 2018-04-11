using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.SimpleMessage
{
    [Export(typeof(IAppMessangerFactrory))]
    [ExportMetadata(ConstVariable.MESSAGE_TYPE, "MockSender")]
    [ExportMetadata(ConstVariable.DOMAIN_NAME, "com.baladorPlant")]
    public class MockSender : AppMessangerFactrory
    {
        [ImportingConstructor()]
        public MockSender(IBaladorContext context, ITaskService taskService) : base(context, taskService)
        {

        }

        public override AuthenticationManager GetAuthenticationManager()
        {
            return new HttpLiteAuthentication(Context, this);
        }

        protected override async Task<ResponseAppMessanger> GetSender(RegisterSender register)
        {
            ResponseAppMessanger response = new ResponseAppMessanger();
            try
            {
                var mockSend = new MockTcpSend(Context,this);
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
