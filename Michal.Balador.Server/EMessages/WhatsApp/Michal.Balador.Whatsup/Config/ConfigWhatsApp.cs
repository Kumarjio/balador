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

namespace Michal.Balador.WhatsApp.Config
{
    public class ConfigWhatsApp: BToken
    {
        public string Phone { get; set; }
     //   public string TempPwsSms { get; set; }
    }
}
