using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
    public class SignUpSender
    {
        public string Id { get; set; }//phone 
        public string Email { get; set; }//for notify message
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
