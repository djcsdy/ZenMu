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
        public string Storyteller { get; set; }
        public List<string> Players { get; set; }
    }
}