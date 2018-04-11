using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Contract;
using Michal.Balador.Contracts.Dal;
using Michal.Balador.Contracts.Mechanism;
using Michal.Balador.Contracts.Service;

namespace Michal.Balador.Contracts.Behaviors
{

    public class RequestBehavior
    {
        public AccountInfo AccountInfo { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public ITaskService TaskService { get; set; }

        public IBaladorContext BaladorContext { get; set; }
    }

    public class RequestContactLoaderBehavior: RequestBehavior
    {
        public List<MessageItem> ListMessages { get; set; }
      
    }

    public class RequestPreMessageBehavior : RequestBehavior
    {
        public MessageItem Message { get; set; }
    
    }

    public class RequestPostMessageBehavior : RequestBehavior
    {
        public MessageItem Message { get; set; }

    }
}
