
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Tracing;

namespace Michal.Balador.Server.Infrastructures
{
    [Export(typeof(Microsoft.AspNet.WebHooks.Diagnostics.ILogger))]
    [Export(typeof(Contracts.Contract.IBaladorLogger))]
    public class Logger : Microsoft.AspNet.WebHooks.Diagnostics.ILogger, Contracts.Contract.IBaladorLogger
    {
        private static readonly log4net.ILog LogTrace = log4net.LogManager.GetLogger(typeof(Michal.Balador.Server.Infrastructures.Logger));

        public void Log(System.Web.Http.Tracing.TraceLevel level, string message, Exception ex)
        {
            // LogTrace.Logger.
            if (level == System.Web.Http.Tracing.TraceLevel.Error)
            {
                LogTrace.Error(message, ex);
            }
            else
            {
                LogTrace.Info(message);

            }
       //     Console.WriteLine("test!!!");
        }

        public void Log(System.Diagnostics.TraceLevel level, string message, Exception ex=null)
        {
            if (level == System.Diagnostics.TraceLevel.Error)
            {
                LogTrace.Error(message, ex);
            }
            else
            {
                LogTrace.Info(message);

            }
            //throw new NotImplementedException();
        }
    }


}
