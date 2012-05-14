using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using ZenMu.Models;

namespace ZenMu.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UsernameAvailable : ValidationAttribute
    {
        public UsernameAvailable() : base("That username is not available.")
        {
        }

        public override bool IsValid(object value)
        {
            var prospectName = value.ToString();
            using (var session = MvcApplication.Store.OpenSession())
            {
                if (session.Query<ZenMuUser>().Any(u => u.Username == prospectName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}