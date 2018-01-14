using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
   public abstract class SenderMessages
    {
        public abstract Task<ResponseSend> Send(SendRequest request);
    }
}
