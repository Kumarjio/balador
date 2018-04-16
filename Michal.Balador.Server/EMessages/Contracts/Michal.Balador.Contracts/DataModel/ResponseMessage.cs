using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{
    public class ResponseMessage : MessageItem
    {
        public ResponseMessage()
        {

        }
        public ResponseMessage(MessageItem messageItem)
        {
            this.AccountId = messageItem.AccountId;
            this.ClientId = messageItem.ClientId;
            this.JobId = messageItem.JobId;
            this.LogThId = messageItem.LogThId;
            this.Message = messageItem.Message;
            this.RecordId = messageItem.RecordId;
            this.Spid = messageItem.Spid;
        }

    public bool IsError { get; set; }
        public string ErrMessage { get; set; }
    }
}
