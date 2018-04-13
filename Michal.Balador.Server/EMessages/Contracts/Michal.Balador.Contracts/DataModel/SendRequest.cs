using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{
   public class SendRequest
    {
        public int Log { get; set; }//email,phone
        public string Id { get; set; }//email,phone
        public List<MessageItem> Messages { get; set; }
        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.AppendFormat("{0},{1}", Id, Log);
            log.AppendLine();
            if (this.Messages != null && this.Messages.Any())
            {
                foreach (var item in this.Messages)
                {
                    log.AppendFormat("{0},{1}", item.ClientId,  item.Message);
                    log.AppendLine();
                }

            }
            return log.ToString();
        }
    }
}
