using System;

namespace ZenMu.ZenMuApp.Messages
{
    public class Command
    {
        public Guid Id { get; set; }
        public CommandType CommandType { get; set; }
        public string Source { get; set; }
        public string[] Subjects { get; set; }
        public string Body { get; set; }
        public DateTime Received { get; set; }
        public string GameName { get; set; }
        public string[] Scenes { get; set; }
    }
}