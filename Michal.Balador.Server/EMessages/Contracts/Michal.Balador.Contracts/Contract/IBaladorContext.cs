using Michal.Balador.Contracts.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts
{
    public interface IBaladorContext
    {
      Task<object>  GetContact(string id);
      Task<ResponseBase> SetContact(object contact);
    }
}
