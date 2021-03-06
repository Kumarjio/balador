﻿//// Copyright (c) .NET Foundation. All rights reserved.
//// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

//using System.ComponentModel;
//using System.Data.Entity;
//using Microsoft.AspNet.WebHooks;
//using Microsoft.AspNet.WebHooks.Config;
//using Microsoft.AspNet.WebHooks.Diagnostics;
//using Microsoft.AspNet.WebHooks.Services;
//using Microsoft.AspNetCore.DataProtection;

//namespace System.Web.Http
//{
//    /// <summary>
//    /// Extension methods for <see cref="HttpConfiguration"/>.
//    /// </summary>
//    [EditorBrowsable(EditorBrowsableState.Never)]
//    public static class HttpSqlConfigurationExtensions
//    {
        

//        /// <summary>
//        /// Configures a Microsoft SQL Server Storage implementation of <see cref="IWebHookStore"/>
//        /// which provides a persistent store for registered WebHooks used by the custom WebHooks module.
//        /// </summary>
//        /// <param name="config">The current <see cref="HttpConfiguration"/>config.</param>
//        /// <param name="encryptData">Indicates whether the data should be encrypted using <see cref="IDataProtector"/> while persisted.</param>
//        /// <param name="nameOrConnectionString">The custom connection string or name of the connection string application setting. Used to initialize <see cref="WebHookStoreContext"/>.</param>
//        /// <param name="schemaName">The custom name of database schema. Used to initialize <see cref="WebHookStoreContext"/>.</param>
//        /// <param name="tableName">The custom name of database table. Used to initialize <see cref="WebHookStoreContext"/>.</param>
//        public static void InitializeBaladorCustomWebHooksSqlStorage(
//            this HttpConfiguration config,
//            bool encryptData,
//            string nameOrConnectionString,
//            string schemaName,
//            string tableName)
//        {
//            if (config == null)
//            {
//                throw new ArgumentNullException(nameof(config));
//            }

//            WebHooksConfig.Initialize(config);

//            //ILogger logger = config.DependencyResolver.GetLogger();
//            //SettingsDictionary settings = config.DependencyResolver.GetSettings();

//            //// We explicitly set the DB initializer to null to avoid that an existing DB is initialized wrongly.
//            //Database.SetInitializer<WebHookStoreContext>(null);

//            //IWebHookStore store;
//            //if (encryptData)
//            //{
//            //    IDataProtector protector = Michal.Balador.Infrastructures.Security.DataSecurity.GetDataProtector();
//            //    store = new SqlWebHookStore(settings, protector, logger, nameOrConnectionString, schemaName, tableName);
//            //}
//            //else
//            //{
//            //    store = new SqlWebHookStore(settings, logger, nameOrConnectionString, schemaName, tableName);
//            //}

//            //CustomServices.SetStore(store);
//        }
//    }
//}
