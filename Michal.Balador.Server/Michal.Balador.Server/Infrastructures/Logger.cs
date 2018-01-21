
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Tracing;

namespace Michal.Balador.Server.Infrastructures
{
    [Export(typeof(Microsoft.AspNet.WebHooks.Diagnostics.ILogger))]
    public class Logger : Microsoft.AspNet.WebHooks.Diagnostics.ILogger
    {
        public void Log(TraceLevel level, string message, Exception ex)
        {

            Console.WriteLine("test!!!");
        }
    }
}
