using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Contract
{
    public interface IBaladorLogger
    {
         void Log(TraceLevel level, string message, Exception ex=null);
    }
}
