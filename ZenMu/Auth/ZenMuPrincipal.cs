using System.Linq;
using System.Security.Principal;

namespace ZenMu.Auth
{
    public class ZenMuPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        private string[] Roles { get; set; }

        public ZenMuPrincipal(ZenMuIdentity identity)
        {
            this.Identity = identity;
        }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        
    }
}