using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Behaviors;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Contracts.Mechanism
{
    public abstract class AppMessanger : IDisposable
    {
        protected IAppMessangerFactrory _provider;
        protected IBaladorContext _context;
        public IBaladorContext Context { get { return _context; } }
        public IAppMessangerFactrory Provider { get { return _provider; } }
        protected List<ContactManager> _contactsManager;

        public AppMessanger(IBaladorContext context, AppMessangerFactrory provider)
        {
            _context = context; _provider = provider;
            _contactsManager = new List<ContactManager>();
        }

        protected virtual ContactManager GetInstanceContactManger(ContactInfo contact)
        {
            return null;
        }
        public async Task<ResponseSend> SendAsync(AccountInfo accountInfo)
        {
            ITaskSchedulerRepository repository = Provider.TaskService.TaskSchedulerRepository;
            var contacts = await repository.GetContacts(accountInfo);
            await LoadContactsManager(contacts.ToList());
            foreach (var contactManagerItem in _contactsManager)
            {
                var messages = await repository.GetMessagesContact(contactManagerItem.ContactInfo);

                foreach (var message_item in messages)
                {
                    await PreSend(accountInfo, contactManagerItem.ContactInfo, message_item);
                    await contactManagerItem.SendMessage(message_item);
                    await PostSend(accountInfo, contactManagerItem.ContactInfo, message_item);
                }

            }
            return new ResponseSend { IsError = false };
        }
        protected async Task PreSend(AccountInfo accountInfo, ContactInfo contact, MessageItem messageItem)
        {
            var preSends = Provider.BehaviorItems?.Get<PreMessageBehavior>();
            if (preSends != null && preSends.Any())
            {
                foreach (var preSend in preSends)
                {
                    var responseMessage = await preSend.Excute(
                        new RequestMessageBehavior
                    {
                        TaskService=Provider.TaskService,
                        AccountInfo=accountInfo,
                        ContactInfo=contact,
                        Message=messageItem
                    });

                }
            }

        }
        protected async Task PostSend(AccountInfo accountInfo, ContactInfo contact, MessageItem messageItem)
        {
            var preSends = Provider.BehaviorItems?.Get<PostMessageBehavior>();
            if (preSends != null && preSends.Any())
            {
                foreach (var preSend in preSends)
                {
                    var responseMessage = await preSend.Excute(
                        new RequestMessageBehavior
                        {
                            TaskService = Provider.TaskService,
                            AccountInfo = accountInfo,
                            ContactInfo = contact,
                            Message = messageItem
                        });

                }
            }

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
