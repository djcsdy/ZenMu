using System;

namespace ZenMu.Auth
{
	public class ZenMuIdentity : System.Security.Principal.IIdentity
	{
        public ZenMuIdentity(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public Guid Id { get; private set; }

	    public string AuthenticationType
	    {
            get { return "Custom"; }
	    }

	    public bool IsAuthenticated
	    {
            get { return !string.IsNullOrEmpty(this.Name); }
	    }
	}
}