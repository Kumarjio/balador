using Michal.Balador.Contracts;
using Michal.Balador.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Infrastructures.Service
{
    public class BaladorContext : IBaladorContext
    {
        public  Task<object> GetContact(string id)
        {
            return null;
            //throw new NotImplementedException();
        }

        public Task<ResponseBase> SaveContact(object contact)
        {
            return null;
            //throw new NotImplementedException();
        }
    }
}
