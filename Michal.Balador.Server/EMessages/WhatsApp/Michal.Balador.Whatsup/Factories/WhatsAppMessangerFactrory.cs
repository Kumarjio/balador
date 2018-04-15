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
using Michal.Balador.Whatsup.Authentication;
using Michal.Balador.Whatsup.ConcreteSender;

namespace Michal.Balador.Whatsup.Factories
{
    [Export(typeof(IAppMessangerFactrory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMetadata(ConstVariable.MESSAGE_TYPE, "Whatsup")]
    [ExportMetadata(ConstVariable.DOMAIN_NAME, "com.baladorPlant")]
    public class WhatsAppMessangerFactrory : AppMessangerFactrory
    {
        [ImportingConstructor()]
        public WhatsAppMessangerFactrory(IBaladorContext context, ITaskService taskService) : base(context, taskService)
        {

        }

        public override AuthenticationManager GetAuthenticationManager()
        {
            return new WhatsAppAuthentication(Context, this);
        }

        protected override async Task<ResponseAppMessanger> GetSender(RegisterSender register)
        {
            ResponseAppMessanger response = new ResponseAppMessanger();
           
            return await Task.FromResult( response);
        }

        protected override async Task<ResponseAppMessanger> GetSender(AccountSend accountSend)
        {
            ResponseAppMessanger response = new ResponseAppMessanger{ IsAutorize = true };
            try
            {
                var whatsAppMessanger = new WhatsAppMessanger(Context, this);
                return await Task.FromResult(new ResponseAppMessanger { IsAutorize=true,IsError=false,Result= whatsAppMessanger});

               // response = await mockSend.dq return response;
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
