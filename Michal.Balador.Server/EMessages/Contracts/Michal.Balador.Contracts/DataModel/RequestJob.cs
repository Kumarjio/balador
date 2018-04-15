using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{
    public class RequestJob
    {
      public AccountSend AccountSend { get; set; }
        public ConcurrentBag<ResponseSend> Retry { get; set; }
    }
}
