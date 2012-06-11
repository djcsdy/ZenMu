using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenMu.Models;
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

            var inGames = RavenSession.Query<Game>().Where(g => g.Players.Contains(user.Id));
            var ownedGames = RavenSession.Query<Game>().Where(g => g.Storyteller == user.Id);
            var activeGames = MvcApplication.Storyteller.GamesContainingPlayer(user.Id).ToList();

            var viewModel = inGames.Concat(ownedGames).Select(game => new GameViewModel { Game = game, IsActive = activeGames.Contains(game.Id) }).ToList();

            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize]
        public JsonResult StartSession(Guid gameId)
        {
            var user = RavenSession.Query<ZenMuUser>().Single(u => u.Username == HttpContext.User.Identity.Name);
            if (IsPlayerStoryteller(user.Id, gameId))
            {
                MvcApplication.Storyteller.CreateGame(gameId);
                return Json(new { Success = true });
            }
            return Json(new { Success = false, ErrorMessage = "Authentication Error" });
        }

        [HttpPost]
        [Authorize]
        public JsonResult EndSession(Guid gameId)
        {
            var user = RavenSession.Query<ZenMuUser>().Single(u => u.Username == HttpContext.User.Identity.Name);
            if (IsPlayerStoryteller(user.Id, gameId))
            {
                MvcApplication.Storyteller.EndGame(gameId);
                return Json(new {Success = true});
            }
            return Json(new {Success = false, ErrorMessage = "Authentication Error"});
        }

        private bool IsPlayerStoryteller(Guid playerId, Guid gameId)
        {
            return RavenSession.Query<Game>().Single(g => g.Id == gameId).Storyteller == playerId;
        }

    }
}
