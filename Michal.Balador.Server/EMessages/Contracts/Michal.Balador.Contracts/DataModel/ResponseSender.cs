using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
    public class ResponseBase
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
    }

    public class Response<T> : ResponseBase
    {
        public T Result { get; set; }
    }

    public class ResponseMessages : Response<SenderMessages>
    {
        

    }
    public class ResponseSend : Response<List<ResponseMessage>>
    {
       
    }

    public class ResponseSender
    {
        public bool IsError { get; set; }
        public string Message { get; set; }

        public List<ResponseMessage> ResponseMessages { get; set; }

    }

    public class ResponseMessage: MessageItem
    {
     
        public bool IsError { get; set; }
        public string ErrMessage { get; set; }
    }
  }
