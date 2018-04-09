using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Infrastructures.Dal
{
    [Export(typeof(ITaskSchedulerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TaskSchedulerRepository : ITaskSchedulerRepository
    {
        protected IUnitOfWork _unitOfWork;
        [ImportingConstructor()]
        public TaskSchedulerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IUnitOfWork DbContext { get { return _unitOfWork; } }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public async Task<List<AccountInfo>> GetAccountsJob()
        {
            var query = "exec [dbo].[jobrunner]";
            var resultSp = await _unitOfWork.Database.SqlQuery<AccountInfo>(query).ToListAsync();
            return resultSp;
        }

        public async Task<IEnumerable<ContactInfo>> GetContacts(AccountInfo accountInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MessageItem>> GetMessagesContact(ContactInfo contactInfo)
        {
            throw new NotImplementedException();
        }
    }
}
