using System;
using System.Collections.Generic;

namespace ZenMu.ViewModels
{
    public class GameViewModel
    {
        public string GameName;
        public Guid GameId;
        public Guid StorytellerId;
        public Dictionary<Guid, string> Players;
        public bool HasSession;
    }
}