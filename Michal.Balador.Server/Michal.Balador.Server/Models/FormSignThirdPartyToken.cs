using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Michal.Balador.Server.Models
{
    public class FormSignThirdPartyToken
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public IDictionary<string,string> Fields { get; set; }

    }
    public class FormsSignThirdPartyToken
    {
        public List<FormSignThirdPartyToken> Forms { get; set; }
    }
}