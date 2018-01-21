using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Michal.Balador.Server.App_Start;
using System.IO;

namespace Michal.Balador.Server
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            MefConfig.RegisterMef();
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ControllerBuilder.Current.SetControllerFactory(new MefControllerFactory(
            //       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins")));
           
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));

        }
    }
}