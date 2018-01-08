using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Michal.Balador.Server.App_Start
{
    public class MefConfig
    {
        public static void RegisterMef()
        {
           // var container = ConfigureContainer();

            //ControllerBuilder.Current.SetControllerFactory(new MefControllerFactory(container));

            //var dependencyResolver = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver;
            //System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new MefDependencyResolver(container);
        }

        //private static CompositionContainer ConfigureContainer()
        //{
        //    //var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
        //    //var businessRulesCatalog = new AssemblyCatalog(typeof(BusinessRules.IValidateMetaData).Assembly);
        //    //var catalogs = new AggregateCatalog(assemblyCatalog, businessRulesCatalog);
        //    //var container = new CompositionContainer(catalogs);
        //    //return container;
        //}
    }
}