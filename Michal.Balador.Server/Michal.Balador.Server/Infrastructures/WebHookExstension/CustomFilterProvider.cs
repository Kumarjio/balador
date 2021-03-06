﻿using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;

namespace Michal.Balador.Server.Infrastructures.WebHookExstension
{
    [Export(typeof(IWebHookFilterProvider))]
    /// <summary>
    /// Use an <see cref="IWebHookFilterProvider"/> implementation to describe the events that users can 
    /// subscribe to. A wildcard filter is always registered meaning that users can register for 
    /// "all events". It is possible to have 0, 1, or more <see cref="IWebHookFilterProvider"/> 
    /// implementations.
    /// </summary>
    public class PrePostFilterProvider : IWebHookFilterProvider
    {
        private readonly Collection<WebHookFilter> filters = new Collection<WebHookFilter>
    {
        new WebHookFilter { Name = BaladorConst.PostUpdate, Description = "This event happened .PostUpdate" },
        new WebHookFilter { Name = BaladorConst.PreUpdate, Description = "This event happened. PreUpdate" },
    };

        public Task<Collection<WebHookFilter>> GetFiltersAsync()
        {
            return Task.FromResult(this.filters);
        }
    }
}