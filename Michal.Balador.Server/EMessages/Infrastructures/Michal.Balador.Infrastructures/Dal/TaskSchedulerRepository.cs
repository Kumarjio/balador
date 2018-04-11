﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Infrastructures.Dal
{
    [Export(typeof(ITaskSchedulerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TaskSchedulerRepository : Repository,ITaskSchedulerRepository
    {
      
        [ImportingConstructor()]
        public TaskSchedulerRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
