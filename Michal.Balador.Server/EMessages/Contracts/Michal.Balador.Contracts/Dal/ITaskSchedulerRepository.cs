using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Contracts.Dal
{
    public interface ITaskSchedulerRepository : IRepository
    {
        Task<List<AccountInfo>> GetAccountsJob();
        Task<IEnumerable<ContactInfo>> GetContacts(AccountInfo accountInfo);
        Task<List<MessageItem>> GetMessagesContact(ContactInfo contactInfo);
    }
}
