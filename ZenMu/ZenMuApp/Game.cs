using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenMu.Models;

namespace ZenMu.ZenMuApp
{
    public class Game
    {
        public string Name;
        public Guid Id;
        public Guid Storyteller { get; set; }
        public List<Guid> Players { get; set; }

        public Game(string name, Guid storyteller)
        {
            Name = name;
            Storyteller = storyteller;
            Players = new List<Guid> {storyteller};
        }
    }
}