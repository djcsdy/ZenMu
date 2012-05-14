using System;

namespace ZenMu.Models
{
    public class ZenMuUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public bool IsDeleted { get; set; }
    }
}