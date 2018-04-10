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
        protected ContactInfo _contact;
        public ContactInfo ContactInfo { get { return _contact; } }
        public ContactManager(SenderMessagesService provider, ContactInfo contact)
        {
            _provider = provider; _contact = contact; 
        }

        public abstract  Task<ResponseBase> Init( );

        public abstract Task<ResponseBase> SendMessage(MessageItem messageItem);
        
    }
}
