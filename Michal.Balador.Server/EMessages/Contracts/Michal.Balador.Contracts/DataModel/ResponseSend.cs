using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
    public class ResponseSend : Response<List<ResponseMessage>>
    {
        public string Id { get; set; }
        public int  Log { get; set; }
    }
}
