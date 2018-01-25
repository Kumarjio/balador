using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dependencies;
using Michal.Balador.Contracts;
using Microsoft.AspNet.WebHooks;
using Microsoft.AspNet.WebHooks.Diagnostics;
using Microsoft.AspNet.WebHooks.Services;
using Microsoft.AspNetCore.DataProtection;

namespace Michal.Balador.Server.App_Start
{
    public class MefDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly CompositionContainer _container;

        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        /// <summary>
        /// Called to request a service implementation.
        /// 
        /// Here we call upon MEF to instantiate implementations of dependencies.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>Service implementation or null.</returns>
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var name = AttributedModelServices.GetContractName(serviceType);
            var export = _container.GetExportedValueOrDefault<object>(name);
            return export;
        }

        /// <summary>
        /// Called to request service implementations.
        /// 
        /// Here we call upon MEF to instantiate implementations of dependencies.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>Service implementations.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var exports = _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
            return exports;
        }

        public void Dispose()
        {
        }
    }

    public class MefConfig
    {
        public static void RegisterMef()
        {
           var container = ConfigureContainer();
            // Install MEF dependency resolver for Web API
            var dependencyResolver = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver;
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new MefDependencyResolver(container);
            //IDependencyScope
            //
            //https://weblog.west-wind.com/posts/2016/Dec/12/Loading-NET-Assemblies-out-of-Seperate-Folders
            Database.SetInitializer<WebHookStoreContext>(null);
            IDataProtector protector = Michal.Balador.Infrastructures.Security.DataSecurity.GetDataProtector();
            ILogger logger = dependencyResolver.GetLogger();

            Microsoft.AspNet.WebHooks.Config.SettingsDictionary settings  = dependencyResolver.GetSettings();

            string nameOrConnectionString = null;
            string schemaName= null;
            string tableName = null;
            var store = new SqlWebHookStore(settings, protector, logger, nameOrConnectionString, schemaName, tableName);

           // CustomServices.SetStore(store);
            container.ComposeExportedValue<IWebHookStore>(store);

        }

        private static CompositionContainer ConfigureContainer()
        {
            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

           var businessRulesCatalog = new AssemblyCatalog(typeof(IEMessage).Assembly);
           var catalogs = new AggregateCatalog(assemblyCatalog, businessRulesCatalog,  new DirectoryCatalog("plugins"));
         
            var container = new CompositionContainer(catalogs);
          return container;
        }
    }
}