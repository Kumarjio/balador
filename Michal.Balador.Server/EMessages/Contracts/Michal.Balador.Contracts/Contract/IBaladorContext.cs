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
        int Log { get; set; }
      Task<object>  GetContact(SenderMessages senderMessages, string id);
      Task<ResponseBase> SetContact(SenderMessages senderMessages,object contact);
    }
}
