using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Contracts.Mechanism
{
   public abstract class ContactManager
    {
        protected AppMessanger _appMessanger;
        protected ContactInfo _contact;
        public ContactInfo ContactInfo { get { return _contact; } }
        public AppMessanger AppMessanger { get { return _appMessanger; } }

        public ContactManager(AppMessanger appMessanger, ContactInfo contact)
        {
            _appMessanger = appMessanger; _contact = contact; 
        }

        public virtual async  Task<ResponseBase> Init()
        {
           return await Task.FromResult(new ResponseBase());
        }


        public abstract Task<ResponseMessage> SendMessage(MessageItem messageItem);
        
    }
}
