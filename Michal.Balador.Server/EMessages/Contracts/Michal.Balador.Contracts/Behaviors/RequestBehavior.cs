using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Behaviors
{

    public class RequestBehavior
    {

    }

    public class RequestContactLoaderBehavior
    {
        public List<MessageItem> ListMessages { get; set; }
        ContactInfo ContactInfo { get; set; }
    }
}
