namespace ZenMu.Models
{
    public class ZenMuUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public bool IsDeleted { get; set; }
    }
}