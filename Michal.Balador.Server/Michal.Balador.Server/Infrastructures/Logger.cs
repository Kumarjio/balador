
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
        private static readonly log4net.ILog LogTrace = log4net.LogManager.GetLogger(typeof(Michal.Balador.Server.Infrastructures.Logger));

        public void Log(TraceLevel level, string message, Exception ex)
        {
            // LogTrace.Logger.
            if (level == TraceLevel.Error)
            {
                LogTrace.Error(message, ex);
            }
            else
            {
                LogTrace.Info(message);

            }
       //     Console.WriteLine("test!!!");
        }
    }
}
