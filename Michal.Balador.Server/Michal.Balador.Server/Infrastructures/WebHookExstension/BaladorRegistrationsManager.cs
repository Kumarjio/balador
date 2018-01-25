﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks.Diagnostics;
using Microsoft.AspNet.WebHooks.Properties;
using Microsoft.AspNet.WebHooks;
using System.ComponentModel.Composition;
using Michal.Balador.Server.Properties;

namespace Michal.Balador.Server.Infrastructures.WebHookExstension
{
    public interface IExposeResult
    {
        IEnumerable<NotificationDictionary> NotificationsResult { get; }
    }

    [Export(typeof(IWebHookManager))]
    /// <summary>
    /// Provides an implementation of <see cref="IWebHookManager"/> for managing notifications and mapping
    /// them to registered WebHooks.
    /// </summary>
    public class BaladorWebHookManager : IWebHookManager, IExposeResult,IDisposable
    {
        internal const string NoEchoParameter = "noecho";
        internal const string EchoParameter = "echo";

        private readonly IWebHookStore _webHookStore;
        private readonly IWebHookSender _webHookSender;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        private bool _disposed;
        [ImportingConstructor]
        /// <summary>
        /// Initialize a new instance of the <see cref="WebHookManager"/> with a default retry policy.
        /// </summary>
        /// <param name="webHookStore">The current <see cref="IWebHookStore"/>.</param>
        /// <param name="webHookSender">The current <see cref="IWebHookSender"/>.</param>
        /// <param name="logger">The current <see cref="ILogger"/>.</param>
        public BaladorWebHookManager(IWebHookStore webHookStore, IWebHookSender webHookSender, ILogger logger)
            : this(webHookStore, webHookSender, logger, httpClient: null)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="WebHookManager"/> with the given <paramref name="httpClient"/>. This 
        /// constructor is intended for unit testing purposes.
        /// </summary>
        internal BaladorWebHookManager(IWebHookStore webHookStore, IWebHookSender webHookSender, ILogger logger, HttpClient httpClient)
        {
            if (webHookStore == null)
            {
                throw new ArgumentNullException(nameof(webHookStore));
            }
            if (webHookSender == null)
            {
                throw new ArgumentNullException(nameof(webHookSender));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _webHookStore = webHookStore;
            _webHookSender = webHookSender;
            _logger = logger;

            _httpClient = httpClient ?? new HttpClient();
        }

        /// <inheritdoc />
        public virtual async Task VerifyWebHookAsync(WebHook webHook)
        {
            if (webHook == null)
            {
                throw new ArgumentNullException(nameof(webHook));
            }

            VerifySecret(webHook.Secret);

            VerifyUri(webHook.WebHookUri);

            await VerifyEchoAsync(webHook);
        }

        /// <inheritdoc />
        public async Task<int> NotifyAsync(string user, IEnumerable<NotificationDictionary> notifications, Func<WebHook, string, bool> predicate)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (notifications == null)
            {
                throw new ArgumentNullException(nameof(notifications));
            }

            // Get all actions in this batch
            ICollection<NotificationDictionary> nots = notifications.ToArray();
            string[] actions = nots.Select(n => n.Action).ToArray();

            // Find all active WebHooks that matches at least one of the actions
            ICollection<WebHook> webHooks = await _webHookStore.QueryWebHooksAsync(user, actions, predicate);

            // For each WebHook set up a work item with the right set of notifications
            IEnumerable<WebHookWorkItem> workItems = GetWorkItems(webHooks, nots);
            _notificationsBagge = new List<NotificationDictionary>();
            // Start sending WebHooks
            await _webHookSender.SendWebHookWorkItemsAsync(workItems);
            foreach (var workItem in workItems)
            {
                foreach (var itemProp in workItem.Properties)
                {
                    _notificationsBagge.Add(new NotificationDictionary(itemProp.Key, new { P4444 = "44444" }));
                 
                }
            }
           
            return webHooks.Count;
        }
        List<NotificationDictionary>   _notificationsBagge;
        public IEnumerable<NotificationDictionary> NotificationsResult {
            get { return _notificationsBagge;
            }
        }
        /// <inheritdoc />
        public async Task<int> NotifyAllAsync(IEnumerable<NotificationDictionary> notifications, Func<WebHook, string, bool> predicate)
        {
            if (notifications == null)
            {
                throw new ArgumentNullException(nameof(notifications));
            }

            // Get all actions in this batch
            ICollection<NotificationDictionary> nots = notifications.ToArray();
            string[] actions = nots.Select(n => n.Action).ToArray();

            // Find all active WebHooks that matches at least one of the actions
            ICollection<WebHook> webHooks = await _webHookStore.QueryWebHooksAcrossAllUsersAsync(actions, predicate);

            // For each WebHook set up a work item with the right set of notifications
            IEnumerable<WebHookWorkItem> workItems = GetWorkItems(webHooks, nots);

            // Start sending WebHooks
            await _webHookSender.SendWebHookWorkItemsAsync(workItems);
            return webHooks.Count;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal static IEnumerable<WebHookWorkItem> GetWorkItems(ICollection<WebHook> webHooks, ICollection<NotificationDictionary> notifications)
        {
            List<WebHookWorkItem> workItems = new List<WebHookWorkItem>();
            foreach (WebHook webHook in webHooks)
            {
                ICollection<NotificationDictionary> webHookNotifications;

                // Pick the notifications that apply for this particular WebHook. If we only got one notification
                // then we know that it applies to all WebHooks. Otherwise each notification may apply only to a subset.
                if (notifications.Count == 1)
                {
                    webHookNotifications = notifications;
                }
                else
                {
                    webHookNotifications = notifications.Where(n => webHook.MatchesAction(n.Action)).ToArray();
                    if (webHookNotifications.Count == 0)
                    {
                        continue;
                    }
                }

                WebHookWorkItem workItem = new WebHookWorkItem(webHook, webHookNotifications);
                workItems.Add(workItem);
            }
            return workItems;
        }

        /// <summary>
        /// Releases the unmanaged resources and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    if (_httpClient != null)
                    {
                        _httpClient.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Verifies the WebHook by submitting a GET request with a query token intended by the echoed back.
        /// </summary>
        /// <param name="webHook">The <see cref="WebHook"/> to verify.</param>
        protected virtual async Task VerifyEchoAsync(WebHook webHook)
        {
            // Create the echo query parameter that we want returned in response body as plain text.
            string echo = Guid.NewGuid().ToString("N");

            HttpResponseMessage response;
            try
            {
                // If WebHook URI contains a "NoEcho" query parameter then we don't verify the URI using a GET request
                NameValueCollection parameters = webHook.WebHookUri.ParseQueryString();
                if (parameters[NoEchoParameter] != null)
                {
                    string msg = string.Format(CultureInfo.CurrentCulture, BaladorResource.Manager_NoEcho);
                    _logger.Info(msg);
                    return;
                }

                // Get request URI with echo query parameter
                UriBuilder webHookUri = new UriBuilder(webHook.WebHookUri);
                webHookUri.Query = EchoParameter + "=" + echo;

                // Create request adding any additional request headers (not entity headers) from Web Hook
                HttpRequestMessage hookRequest = new HttpRequestMessage(HttpMethod.Get, webHookUri.Uri);
                foreach (var kvp in webHook.Headers)
                {
                    hookRequest.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);
                }

                response = await _httpClient.SendAsync(hookRequest);
            }
            catch (Exception ex)
            {
                string msg = string.Format(CultureInfo.CurrentCulture, BaladorResource.Manager_VerifyFailure, ex.Message);
                _logger.Error(msg, ex);
                throw new InvalidOperationException(msg);
            }

            if (!response.IsSuccessStatusCode)
            {
                string msg = string.Format(CultureInfo.CurrentCulture, BaladorResource.Manager_VerifyFailure, response.StatusCode);
                _logger.Info(msg);
                throw new InvalidOperationException(msg);
            }

            // Verify response body
            if (response.Content == null)
            {
                string msg = BaladorResource.Manager_VerifyNoBody;
                _logger.Error(msg);
                throw new InvalidOperationException(msg);
            }

            string actualEcho = await response.Content.ReadAsStringAsync();
            if (!string.Equals(actualEcho, echo, StringComparison.Ordinal))
            {
                string msg = BaladorResource.Manager_VerifyBadEcho;
                _logger.Error(msg);
                throw new InvalidOperationException(msg);
            }
        }

        /// <summary>
        /// Verifies that the <paramref name="webHookUri"/> has either an 'http' or 'https' scheme.
        /// </summary>
        /// <param name="webHookUri">The URI to verify.</param>
        protected virtual void VerifyUri(Uri webHookUri)
        {
            // Check that WebHook URI scheme is either '<c>http</c>' or '<c>https</c>'.
            if (!(webHookUri.IsHttp() || webHookUri.IsHttps()))
            {
                string msg = string.Format(CultureInfo.CurrentCulture, BaladorResource.Manager_NoHttpUri, webHookUri);
                _logger.Error(msg);
                throw new InvalidOperationException(msg);
            }
        }

        /// <summary>
        /// Verifies that the <see cref="WebHook"/> secret is between 32 and 64 characters long.
        /// </summary>
        /// <param name="secret">The <see cref="WebHook"/> secret to validate.</param>
        protected virtual void VerifySecret(string secret)
        {
            // Check that we have a valid secret
            if (string.IsNullOrEmpty(secret) || secret.Length < 32 || secret.Length > 64)
            {
                throw new InvalidOperationException(BaladorResource.WebHook_InvalidSecret);
            }
        }
    }
}
