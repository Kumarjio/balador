using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.DataModel
{
    public class LogInfo
    {
        public string AccountId { get; set; }
        public int Spid { get; set; }
        public Guid JobId { get; set; }
        public int LogThId { get; set; }

    }
}
