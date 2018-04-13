using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Mechanism
{
    public class MessageItem: LogInfo
    {
        public Guid RecordId { get; set; }
        public string ClientId { get; set; }
        public string Message { get; set; }
    }
}
