using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{
    public class ResponseAppMessanger : Response<AppMessanger>
    {
        public bool IsAutorize { get; set; }

    }
}
