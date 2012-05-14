using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenMu.ZenMuApp
{
    public class Message
    {
        public MessageType MessageType;
        public string Source;
        public string[] Subjects;
        public string Body;
        public DateTime Received;
        public string GameName;
        public string[] Scenes;
    }
}