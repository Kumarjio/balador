using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
   public class ResponseSender
    {
        public bool IsError { get; set; }
        public string Message { get; set; }

        public List<ResponseMessage> ResponseMessages { get; set; }

    }

    public class ResponseMessage
    {
        public string Id { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
  }
