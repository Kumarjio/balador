using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Mechanism
{
    public class AuthenticationUser
    {
        public bool IsTwoFactorAuthentication { get; set; }
        public bool IsAuthenticated{ get; set; }
        public string UserId { get; set; }
    }
}
