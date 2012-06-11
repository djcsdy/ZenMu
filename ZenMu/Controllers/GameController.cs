using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenMu.Models;
using ZenMu.ZenMuApp;

namespace ZenMu.Controllers
{
    public class GameController : RavenController
    {
        //
        // GET: /Game/
        [Authorize]
        public ActionResult Index()
        {
            var user = RavenSession.Query<ZenMuUser>().Single(u => u.Username == HttpContext.User.Identity.Name);

            var inGames = RavenSession.Query<Game>().Where(g => g.Players.Contains(user.Id));
            var ownedGames = RavenSession.Query<Game>().Where(g => g.Storyteller == user.Id);
            var activeGames = MvcApplication.GameServer.GamesContainingPlayer(user.Id).ToList();

            var viewModel = inGames.Concat(ownedGames).Select(game => new GameViewModel { Game = game, IsActive = activeGames.Contains(game.Id) }).ToList();

            return View(viewModel);
        }

    }
}
