using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Service
{
   public abstract class ContactManager
    {
        protected SenderMessagesService _provider;

        public ContactManager(SenderMessagesService provider)
        {
            _provider = provider;
        }

        public abstract  Task<ResponseBase> Init(ContactInfo contact);

        public abstract Task<ResponseBase> SendMessage(MessageInfo messageInfo);


    }
}
