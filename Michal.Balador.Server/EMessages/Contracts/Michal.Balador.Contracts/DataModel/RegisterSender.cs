using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
   public class RegisterSender
    {
      // public bool IsAuthenticate { get; set; }
        public int Log { get; set; }
        public string Id { get; set; }//email,phone
        //public string Pws { get; set; }//token
        public string Email { get; set; }//email
        public List<KeyValuePair<string,string>> Extra { get; set; }

    }
}
