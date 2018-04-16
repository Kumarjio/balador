using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.NSWhatsApp.ConcreteSender;
using WhatsAppApi;
using WhatsAppApi.Account;
using WhatsAppApi.Helper;
using WhatsAppApi.Register;
using WhatsAppApi.Response;
namespace Michal.Balador.NSWhatsApp.ConcreteContact
{
    public class WhatsAppContactManager : ContactManager
    {
        public WhatsAppContactManager(AppMessanger appMessanger, ContactInfo contact) : base(appMessanger, contact)
        {
          
        }

        public override async Task<ResponseMessage> SendMessage(MessageItem messageItem)
        {
            var whatsAppMessanger = (WhatsAppMessanger)this.AppMessanger;
            var wa = whatsAppMessanger.WhatsApp;
            await Task.Run(() =>
            {
                try
                {
                    WhatsUserManager usrMan = new WhatsUserManager();
                    var tmpUser = usrMan.CreateUser(messageItem.ClientId, "User");
                    wa.SendMessage(tmpUser.GetFullJid(), messageItem.Message);
             
                    return new ResponseMessage (messageItem);
                }
                catch (Exception e)
                {
                    return new ResponseMessage(messageItem);
                }
            });
            return new ResponseMessage { ClientId = messageItem.ClientId, IsError = true, ErrMessage = "", Message = messageItem.Message };

        }
    }
}
