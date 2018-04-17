
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using lior.api.Models;
using System.ComponentModel.Composition;
using Michal.Balador.Contracts.Dal;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Michal.Balador.Infrastructures.Dal
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountRepository : Repository, IAccountRepository
    {

        [ImportingConstructor()]
        public AccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<string> GetUserId(string username)
        {
            ApplicationDbContext context= this.DbContext as ApplicationDbContext;
            var userId =await  context.Users.Where(d => d.UserName == username).Select(s=>s.Id).FirstOrDefaultAsync();
            return userId;
        }

      
    }


}