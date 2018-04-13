using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.DataModel;

namespace Michal.Balador.Contracts.Mechanism
{
   public class ContactInfo: LogInfo
    {
       // public Guid? LeadId { get; set; } // from lead table
        public string ClientId { get; set; } // clientid (phone or email)username on user table or clientid from lead table
        public string ContactId { get; set; } // contact from user table if exsst
       // public Guid JobId { get; set; } // from request
        public string MesssageType { get; set; } // from request
       // public bool  IsAutorize { get; set; }// is exist on user table
        public string NickName { get; set; }
      
    }
}
