using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{
   public class ContactInfo
    {
        public string Id { get; set; } // clientid (phone or email)username on user table or clientid from lead table
        public string AccountId { get; set; }  //account from user table if exsst
        public string ContactId { get; set; } // contact from user table if exsst
        public string NickName { get; set; }
        public bool  IsAutorize { get; set; }// is exist on user table
        public Guid? LeadId { get; set; } // from lead table
        public Guid JobId { get; set; } // from request
        public string MesssageType { get; set; } // from request


        

    }
}
