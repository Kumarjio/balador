using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Michal.Balador.Server.ViewLog
{
    /// <summary>
    /// Summary description for vlog
    /// </summary>
    public class vlog : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var defaultLogFolder = HttpContext.Current.Server.MapPath("~/Logs");//  System.Configuration.ConfigurationManager.AppSettings["DefaultLogFolder"];
            string[] fileEntries = Directory.GetFiles(defaultLogFolder);
            context.Response.ContentType = "text/html";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html><body>List <br/>");
            foreach (string fileName in fileEntries)
            {
                var f = Path.GetFileName(fileName);
                sb.AppendFormat("<a href='/Logs/{0}'>{0}</a><br/>", f);
            }
            sb.AppendLine("</body></html>");

            context.Response.Write(sb.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}