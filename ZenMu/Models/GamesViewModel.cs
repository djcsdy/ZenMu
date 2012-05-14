using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenMu.ZenMuApp;

namespace ZenMu.Models
{
    public class GamesViewModel
    {
        public List<Game> OwnGames;
        public List<Game> InGames;

        public GamesViewModel()
        {
            OwnGames = new List<Game>();
            InGames = new List<Game>();
        }
    }
}