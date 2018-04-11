using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{

    public class ResponseBase
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}
