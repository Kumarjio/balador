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

        public override string ToString()
        {
            StringBuilder log = new StringBuilder();
            log.AppendFormat("{0},{1}", Id, Log);
            log.AppendLine();
            if (this.Result!=null && this.Result.Any())
            {
                foreach (var item in this.Result)
                {
                    log.AppendFormat("{0},{1},{2}", item.Id, item.IsError, item.Message);
                    log.AppendLine();
                }
                
            }
            return log.ToString();
        }
    }
}
