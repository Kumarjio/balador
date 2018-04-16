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
using Michal.Balador.WhatsApp.Authentication;
using Michal.Balador.WhatsApp.ConcreteSender;

namespace Michal.Balador.WhatsApp.Factories
{
    [Export(typeof(IAppMessangerFactrory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMetadata(ConstVariable.MESSAGE_TYPE, "WhatsApp")]
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
