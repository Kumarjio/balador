using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
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
        protected List<ContactManager> _contactsManager;
        public SenderMessagesService(IBaladorContext context, FactrorySendMessages provider)
        {
            _context = context; _provider = provider;
            _contactsManager = new List<ContactManager>();
        }

        protected virtual ContactManager GetInstanceContactManger(ContactInfo contact)
        {
            return null;
        }
        public async Task<ResponseSend> SendAsync(IContactRepository repository, AccountInfo accountInfo)
        {
            var contacts = await repository.GetContacts(accountInfo);
            await LoadContactsManager(contacts.ToList());
            foreach (var contactManagerItem in _contactsManager)
            {
                var messages = await repository.GetMessagesContact(contactManagerItem.ContactInfo);
                var preSends = Provider.BehaviorItems?.Get<PreMessageBehavior>();

                foreach (var message_item in messages)
                {
                    if (preSends != null && preSends.Any())
                    {
                        foreach (var preSend in preSends)
                        {
                           var responseMessage= await preSend.ChangeMessage(message_item);

                        }
                    }      
                
                   await contactManagerItem.SendMessage(message_item);
                }

            }
            return new ResponseSend { IsError = false };
        }
        public async Task<ResponseSend> LoadContactsManager(List<ContactInfo> contacts)
        {
            foreach (var contact in contacts)
            {
                ContactManager contactManager = GetInstanceContactManger(contact);
                var contactsender = await contactManager.Init();

                _contactsManager.Add(contactManager);

            }
            return new ResponseSend { IsError = false };
        }

        public abstract void Dispose();

        public abstract Task<ResponseSend> Send(SendRequest request);

        public virtual AuthenticationManager GetAuthenticationManager()
        {
            return _provider.GetAuthenticationManager();
        }
    }
}
