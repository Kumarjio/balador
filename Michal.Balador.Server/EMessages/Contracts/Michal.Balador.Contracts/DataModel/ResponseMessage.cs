using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{
    public class ResponseMessage : MessageItem
    {

        public bool IsError { get; set; }
        public string ErrMessage { get; set; }
    }
}
