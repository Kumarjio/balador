using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Mechanism
{
    public class AccountInfo: LogInfo
    {
        public int ManagedThreadId { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string MessagesType { get; set; }
        public string UserName { get; set; }
        public Guid JobId { get; set; }
    }

    public class AccountSend: AccountInfo
    {
        public string Messassnger { get; set; }
        public string MessaggerShrotName { get; set; }
    }
}
