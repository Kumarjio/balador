using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Michal.Balador.Contracts.Mechanism;

namespace Michal.Balador.Infrastructures.Utils
{
    public class Utils
    {
        internal static string GetClaim(ClaimsPrincipal principal, string claimsType)
        {
            Claim claim = principal != null ? principal.FindFirst(claimsType) : null;
            return claim != null ? claim.Value : null;
        }

        /// <inheritdoc />
        public static Task<string> GetUserIdAsync(IPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            string id = null;
            ClaimsPrincipal principal = user as ClaimsPrincipal;
            if (principal != null)
            {
                id = GetClaim(principal, ClaimTypes.Name);
                if (id == null)
                {
                    id = GetClaim(principal, ClaimTypes.NameIdentifier);
                }
            }

            // Fall back to name property
            if (id == null && user.Identity != null)
            {
                id = user.Identity.Name;
            }

            if (id == null)
            {
                string msg = "No User";
                throw new InvalidOperationException(msg);
            }

            return Task.FromResult(id);
        }
    
    }
}
