using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Whatsup.ConcreteContact;
using Michal.Balador.Whatsup.Config;
using WhatsAppApi;

namespace Michal.Balador.Whatsup.ConcreteSender
{
    public class WhatsAppMessanger : AppMessanger
    {
        WhatsApp wa;
        public WhatsAppMessanger(IBaladorContext context, AppMessangerFactrory provider) : base(context, provider)
        {

     
        }

        protected virtual ContactManager GetInstanceContactManger(ContactInfo contact)
        {
            return new WhatsAppContactManager(this, contact);
        }
        public override void Dispose()
        {
            
        }

        public override async Task<ResponseAppMessanger> GetSender(AccountSend accountSend)
        {
            ResponseAppMessanger response = new ResponseAppMessanger { IsAutorize = true };
            try
            {
                var getAuthenticationManager=GetAuthenticationManager();
                var token=await getAuthenticationManager.GetToken(new SignUpSender { UserName=accountSend.UserName});
                if(token!=null && token.Token != null && token is ConfigWhatsApp)
                {
                    ConfigWhatsApp config = (ConfigWhatsApp)token;
                    wa = new WhatsApp(config.Phone, config.Token, accountSend.Name, true, false);

                    return await Task.FromResult(new ResponseAppMessanger());
                }
              //  wa = new WhatsApp(accountSend.
                return response;
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Message = e.Message;
            }
            return response;
        }

        public override async Task<ResponseSend> Send(SendRequest request)
        {
            // wa = new WhatsApp(request.
            return await Task.FromResult(new ResponseSend());
        }
    }
}
