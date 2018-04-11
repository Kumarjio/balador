using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lior.api.Models;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Infrastructures.Dal;

namespace Michal.Balador.Infrastructures.Service
{
    public class JobManager
    {
        JobRepository _jobRepository;
           log4net.ILog _logger; ApplicationDbContext _context;
        public JobManager(log4net.ILog logger, ApplicationDbContext context)
        {
            _logger = logger;_context = context;
            _jobRepository = new JobRepository(_logger,_context);
        }
        public async Task  JobRun(IEnumerable<Lazy<IAppMessangerFactrory, IDictionary<string, object>>> senderRules)
        {
            try
            {
                var jobaccounts=await _jobRepository.GetAccountsJob();
                if(jobaccounts!=null && jobaccounts.Any())
                {
                    foreach (var jobaccount in jobaccounts)
                    {
                        if (String.IsNullOrEmpty(jobaccount.Id))
                            continue;

                    }
                }


            }
            catch (Exception ee)
            {

                throw;
            }
        }
    }
}
