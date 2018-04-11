using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{
   
    public class Response<T> : ResponseBase
    {
        public T Result { get; set; }
     
    }
}
