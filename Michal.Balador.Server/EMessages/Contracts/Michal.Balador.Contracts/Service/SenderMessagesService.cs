using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.Contracts.DataModel
{
    public abstract class SenderMessagesService : IDisposable
    {
        protected IFactrorySendMessages _provider;
        protected IBaladorContext _context;
        public IBaladorContext Context { get { return _context; } }
        public IFactrorySendMessages Provider { get { return _provider; } }
        protected List<ContactManager> _contacts;
        public SenderMessagesService(IBaladorContext context, FactrorySendMessages provider)
        {
            _context = context; _provider = provider;
            _contacts = new List<ContactManager>();
        }

        protected virtual ContactManager GetInstanceContactService()
        {
            return null;
        }

        public virtual async Task<ResponseSend> SendMessages(List<ContactInfo> request)
        {
            foreach (var contact in request)
            {
                ContactManager contactService = GetInstanceContactService();
               var contactsender=await contactService.Init(contact);
                _contacts.Add(contactService);

            }
            return null;
        }


        //public virtual string ServiceName
        //{
        //    get
        //    {
        //        //System.Attribute[] attrs = System.Attribute.GetCustomAttributes(_provider.GetType());  // Reflection.  
        //        //var message_type = "";

        //        //var domain = "";
        //        //foreach (System.Attribute attr in attrs)
        //        //{
        //        //    if (attr is ExportMetadataAttribute)
        //        //    {
        //        //        ExportMetadataAttribute a = (ExportMetadataAttribute)attr;
        //        //        if (a.Name == ConstVariable.MESSAGE_TYPE)
        //        //        {
        //        //            message_type = a.Value.ToString();
        //        //        }
        //        //        else if (a.Name == ConstVariable.DOMAIN_NAME)
        //        //        {
        //        //            domain = a.Value.ToString();
        //        //        }
        //        //    }
        //        //}

        //        //return $"{domain}.{message_type}";//this.GetType().FullName;
        //        return _provider.ServiceName;
        //    }
        //}


        public abstract void Dispose();

        public abstract Task<ResponseSend> Send(SendRequest request);
        
        public virtual AuthenticationManager GetAuthenticationManager()
        {
            return _provider.GetAuthenticationManager();
        }
    }
}
