using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
   public class ContactInfo
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public bool  IsAutorize { get; set; }
    }
}
