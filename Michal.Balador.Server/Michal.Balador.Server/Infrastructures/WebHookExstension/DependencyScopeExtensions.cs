//// Cyright (c) .NET Foundation. All rights reserved.
//// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using System.Web.Http.Dependencies;
//using Microsoft.AspNet.WebHooks.Diagnostics;
//using Microsoft.AspNet.WebHooks.Services;
//using Microsoft.AspNet.WebHooks;
//namespace Michal.Balador.Server.Infrastructures.WebHookExstension
//{
//    /// <summary>
//    /// Extension methods for <see cref="IDependencyScope"/> facilitating getting the services used by custom WebHooks.
//    /// </summary>
//  //  [EditorBrowsable(EditorBrowsableState.Never)]
//    public   static partial class  DependencyScopeExtensions
//    {
       
//        /// <summary>
//        /// Gets an <see cref="IWebHookRegistrationsManager"/> implementation registered with the Dependency Injection engine
//        /// or a default implementation if none are registered.
//        /// </summary>
//        /// <param name="services">The <see cref="IDependencyScope"/> implementation.</param>
//        /// <returns>The registered <see cref="IWebHookRegistrationsManager"/> instance or a default implementation if none are registered.</returns>
//        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed by caller.")]
//        public static IWebHookRegistrationsManager GetBaladorRegistrationsManager(this IDependencyScope services)
//        {
//            ///IWebHookRegistrationsManager registrationsManager = services.GetService<IWebHookRegistrationsManager>();
//            //if (registrationsManager == null)
//           // {
//                IWebHookManager manager = services.GetManager();
//                IWebHookStore store = services.GetStore();
//                IWebHookFilterManager filterManager = services.GetFilterManager();
//                IWebHookUser userManager = services.GetUser();
//                // registrationsManager = CustomServices.GetSender(manager, store, filterManager, userManager);
//                IWebHookRegistrationsManager registrationsManager = new BaladorRegistrationsManager(manager, store, filterManager, userManager);

//                CustomServices.SetRegistrationsManager(registrationsManager);
//          //  }
//            return registrationsManager;
//        }
//    }
//}
