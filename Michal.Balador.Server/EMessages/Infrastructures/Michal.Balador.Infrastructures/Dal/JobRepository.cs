using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lior.api.Models;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Infrastructures.Dal
{
   public class JobRepository
    {
        log4net.ILog _logger; ApplicationDbContext _context;
        public JobRepository(log4net.ILog logger, ApplicationDbContext context)
        {
            _logger = logger; _context = context;
        }

        public async Task<List<AccountInfo>> GetAccountsJob()
        {
           var query = "exec [dbo].[jobrunner]";
            var resultSp = await _context.Database.SqlQuery<AccountInfo>(query).ToListAsync();
            return resultSp;
        }
    }
}
