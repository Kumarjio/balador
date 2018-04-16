using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts;
using Michal.Balador.Contracts.Mechanism;

namespace  Michal.Balador.Telegram.Config
{
    public class ConfigTelegram : BToken
    {
        public int Api { get; set; }
        public string Api_Hash { get; set; }
        public string Phone { get; set; }
        public string CodeRequest { get; set; }

        
        public string Session { get; set; }


    }
}
