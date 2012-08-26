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

            //var inGames = RavenSession.Query<Game>().ToList().Where(g => g.Players.Exists(p => p == user.Id));
            var ownedGames = RavenSession.Query<Game>().Where(g => g.Storyteller == user.Id).ToList();
            var activeGames = MvcApplication.Storyteller.GamesContainingPlayer(user.Id).ToList();

            var viewModel = ownedGames.Select(game => new GameViewModel
                                                                          {
                                                                              GameName = game.Name, 
                                                                              StorytellerId = game.Storyteller,
                                                                              HasSession = activeGames.Contains(game.Id),
                                                                              Players = new Dictionary<Guid, string>()
                                                                          }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult New(string name)
        {
            Guid userId = RavenSession.Query<ZenMuUser>().Single(u => u.Username == HttpContext.User.Identity.Name).Id;
            Game theGame = new Game(name, userId);
            RavenSession.Store(theGame);
            return RedirectToAction("Index");
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
