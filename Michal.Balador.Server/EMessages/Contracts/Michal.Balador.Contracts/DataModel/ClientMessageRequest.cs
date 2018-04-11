using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Michal.Balador.Contracts.Mechanism
{
    public class MessageRequest
    {
        public string User { get; set; }
        public string NickName { get; set; }
        public string ClientId { get; set; }
        public string Messsage { get; set; }
        public string MesssageType { get; set; }
        

    }
}