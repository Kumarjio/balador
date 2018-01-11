using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
   public class Sender
    {
        public string Id { get; set; }//email,phone

        public List<MessageItem> Messages { get; set; }


    }
}
