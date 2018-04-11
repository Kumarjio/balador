﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }

    public class RequestContactLoaderBehavior: RequestBehavior
    {
        public List<MessageItem> ListMessages { get; set; }
      
    }

    public class RequestMessageBehavior : RequestBehavior
    {
        public MessageItem Message { get; set; }
    
    }
}
