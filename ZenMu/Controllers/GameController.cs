using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenMu.Models;
using ZenMu.ViewModels;
using ZenMu.ZenMuApp;
using Newtonsoft;
using Newtonsoft.Json;

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
			
			return View(GetOwnedGamesForPlayerId(user.Id));
        }

		[Authorize]
		public ActionResult Play(string id)
		{
			return View();
		}

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult New(string name)
        {
            Guid userId = RavenSession.Query<ZenMuUser>().Single(u => u.Username == HttpContext.User.Identity.Name).Id;
            Game theGame = new Game(name, userId);
            RavenSession.Store(theGame);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public PartialViewResult StartSession(Guid gameId)
        {
            var user = RavenSession.Query<ZenMuUser>().Single(u => u.Username == HttpContext.User.Identity.Name);
            if (IsPlayerStoryteller(user.Id, gameId))
            {
                MvcApplication.Storyteller.CreateGame(gameId);
            }
			return PartialView("OwnedGames", GetOwnedGamesForPlayerId(user.Id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public PartialViewResult EndSession(Guid gameId)
        {
            var user = RavenSession.Query<ZenMuUser>().Single(u => u.Username == HttpContext.User.Identity.Name);
            if (IsPlayerStoryteller(user.Id, gameId))
            {
                MvcApplication.Storyteller.EndGame(gameId);
            }
        	return PartialView("OwnedGames", GetOwnedGamesForPlayerId(user.Id));
        }

        private bool IsPlayerStoryteller(Guid playerId, Guid gameId)
        {
            return RavenSession.Query<Game>().Single(g => g.Id == gameId).Storyteller == playerId;
        }

		private List<GameViewModel> GetOwnedGamesForPlayerId(Guid id)
		{
			var ownedGames = RavenSession.Query<Game>().Where(g => g.Storyteller == id).ToList();
			var activeGames = MvcApplication.Storyteller.GamesContainingPlayer(id).ToList();

			return ownedGames.Select(game => new GameViewModel
				{
					GameName = game.Name,
					GameId = game.Id,
					StorytellerId = game.Storyteller,
					HasSession = activeGames.Contains(game.Id),
					Players = new Dictionary<Guid, string>()
				}).ToList();
			
		}

    }
}
