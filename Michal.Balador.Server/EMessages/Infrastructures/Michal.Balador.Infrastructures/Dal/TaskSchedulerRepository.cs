using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Infrastructures.Dal
{
    [Export(typeof(ITaskSchedulerRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TaskSchedulerRepository : Repository, ITaskSchedulerRepository
    {

        [ImportingConstructor()]
        public TaskSchedulerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
        }

        public async Task<ResponseBase> Complete(Guid jobid)
        {
            ResponseBase responseBase = new ResponseBase();
            try
            {
                List<object> parameters = new List<object>();
                var query = "exec [dbo].[job_complete] @jobid";
                parameters.Add(new SqlParameter("@jobid", jobid));
                var resultSp = await _unitOfWork.Database.SqlQuery<object>(query, parameters.ToArray()).FirstOrDefaultAsync();

            }
            catch (Exception ee)
            {
                responseBase.IsError = true;
                responseBase.Message = ee.Message;
            }
            return responseBase;
        }

        public async Task<List<AccountInfo>> GetAccountsJob()
        {
            var query = "exec [dbo].[jobrunner]";
            if (_unitOfWork.Database.Connection != null)
            {
                var resultSp = await _unitOfWork.Database.SqlQuery<AccountInfo>(query).ToListAsync();
                return resultSp;
            }
            return null;
     
        }

        public async Task<IEnumerable<ContactInfo>> GetContacts(AccountSend accountInfo)
        {
            try
            {
                List<object> parameters = new List<object>();
                var query = "exec [dbo].[balador_sp_getContacts] @jobid,@messassnger,@accountid ";
                parameters.Add(new SqlParameter("@jobid", accountInfo.JobId));
                parameters.Add(new SqlParameter("@messassnger", accountInfo.Messassnger));
                parameters.Add(new SqlParameter("@accountid", accountInfo.Id));
                if (_unitOfWork.Database.Connection != null)
                {
                    return await _unitOfWork.Database.SqlQuery<ContactInfo>(query, parameters.ToArray()).ToListAsync();
                }
                return null;
              
            } 

            catch (Exception ee)
            {

                throw ee;
            }
           
        }

        public async Task<List<MessageItem>> GetMessagesContact(ContactInfo contactInfo)
        {
            var dt = DateTime.UtcNow;
            List<object> parameters = new List<object>();
            var query = "exec [dbo].[balador_sp_getMessages] @jobid,@messassnger,@accountid,@clientid";
            parameters.Add(new SqlParameter("@jobid", contactInfo.JobId));
            parameters.Add(new SqlParameter("@messassnger", contactInfo.MesssageType));
            parameters.Add(new SqlParameter("@accountid", contactInfo.AccountId));
            parameters.Add(new SqlParameter("@clientid", contactInfo.Id));

            return await _unitOfWork.Database.SqlQuery<MessageItem>(query, parameters.ToArray()).ToListAsync();

        }
    }
}
