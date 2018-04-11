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
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.SimpleMessage
{
    [Export(typeof(IAppMessangerFactrory))]
    [ExportMetadata(ConstVariable.MESSAGE_TYPE, "MockHttpSender")]
    [ExportMetadata(ConstVariable.DOMAIN_NAME, "com.baladorPlant")]
    public class MockHttpSender : AppMessangerFactrory
    {
        [ImportingConstructor()]
        public MockHttpSender(IBaladorContext context, ITaskService taskService) : base(context, taskService)
        {
        }

        public override AuthenticationManager GetAuthenticationManager()
        {
             return new HttpSimpleAuthentication(Context,this);
        }

        protected async override Task<ResponseAppMessanger> GetSender(RegisterSender register)
        {
            ResponseAppMessanger response = new ResponseAppMessanger();
            try
            {
                var mckHttpSend = new MockHttpSend(this.Context,this);
                response.Result = mckHttpSend;
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Message = e.Message;
            }
            return await  Task.FromResult<ResponseAppMessanger>( response);
        }

        protected override async Task<ResponseAppMessanger> GetSender(AccountSend accountSend)
        {
            ResponseAppMessanger response = new ResponseAppMessanger();
            try
            {
                var mckHttpSend = new MockHttpSend(this.Context, this);
                response.Result = mckHttpSend;
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Message = e.Message;
            }
            return await Task.FromResult<ResponseAppMessanger>(response);
        }
    }
}