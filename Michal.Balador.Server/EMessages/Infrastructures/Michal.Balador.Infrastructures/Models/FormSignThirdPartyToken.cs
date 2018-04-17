using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Server.Models
{
    public class FormSignThirdPartyToken
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public List<FieldView> Fields { get; set; }
        public bool  IsAlreadyRegister { get; set; }
        public bool TwoFactorAuthentication { get; set; }
        public string Agreement { get; set; }
        public bool  IsAgreement { get; set; }
        public string HelpFile { get; set; }
    }
    public class FormsSignThirdPartyToken
    {
        public List<FormSignThirdPartyToken> Forms { get; set; }
    }
}