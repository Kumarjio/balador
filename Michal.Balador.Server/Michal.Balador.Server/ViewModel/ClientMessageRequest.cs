﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Michal.Balador.Server.ViewModel
{
    public class ClientMessageRequest
    {
        public string NickName { get; set; }
        public string ClientId { get; set; }
        public string Messsage { get; set; }
        public string MesssageType { get; set; }
        

    }
}