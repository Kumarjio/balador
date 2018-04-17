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
using Michal.Balador.NSWhatsApp.Authentication;
using Michal.Balador.NSWhatsApp.ConcreteSender;
using Michal.Balador.NSWhatsApp.Config;
using WhatsAppApi;

namespace Michal.Balador.NSWhatsApp.Factories
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
            ResponseAppMessanger response = new ResponseAppMessanger { IsAutorize = true };
            try
            {
                var config = await this.Context.GetConfiguration<ConfigWhatsApp>(ServiceName, accountSend.UserName);
                SenderWhatsAppMessagesFactory messagesFactory = new SenderWhatsAppMessagesFactory(this.Context);

                var resConnect = await messagesFactory.ConnectAndLogin(config.Phone, config.Token, accountSend.Name);
                if (resConnect.IsError)
                {
                    response.IsError = true;
                    response.Message = resConnect.Message;
                    return response;
                }
                var WhatsAppMessanger = new WhatsAppMessanger(Context, this, resConnect.Result);
                return await Task.FromResult(new ResponseAppMessanger { IsAutorize = true, IsError = false, Result = WhatsAppMessanger });
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
