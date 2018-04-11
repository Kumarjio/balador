// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Michal.Balador.Infrastructures.Security
{
    /// <summary>
    /// Provides utility functions for the Data Protection functionality in ASP.NET Core 1.0
    /// </summary>
    public static class DataSecurity
    {
        private const string Purpose = "WebHookPersistence";

        private static IDataProtector _dataProtector;

        /// <summary>
        /// This follows the same initialization that is provided when <see cref="IDataProtectionProvider"/>
        /// is initialized within ASP.NET Core 1.0 Dependency Injection.
        /// </summary>
        /// <returns>A fully initialized <see cref="IDataProtectionProvider"/>.</returns>
        public static IDataProtector GetDataProtector(string path)
        {
            if (_dataProtector != null)
            {
                return _dataProtector;
            }

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection()
             .PersistKeysToFileSystem(new DirectoryInfo(path));
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            IDataProtectionProvider provider = services.GetDataProtectionProvider();
            IDataProtector instance = provider.CreateProtector(Purpose);
            Interlocked.CompareExchange(ref _dataProtector, instance, null);
            return _dataProtector;
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }


}
