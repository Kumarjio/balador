using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
   public class RegisterSender
    {
        public string Id { get; set; }//email,phone
        public List<KeyValuePair<string,string>> Extra { get; set; }

    }
}
