using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
    public class ResponseSenderMessages : Response<SenderMessages>
    {
        public bool IsAutorize { get; set; }

    }
}
