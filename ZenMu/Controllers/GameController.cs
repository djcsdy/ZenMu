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

        public ActionResult Index()
        {
            var viewModel = new GamesViewModel();
            viewModel.OwnGames = RavenSession.Query<Game>().Where(u => u.Storyteller == HttpContext.User.Identity.Name).ToList();
            viewModel.InGames = RavenSession.Query<Game>().Where(u => u.Players.Contains(HttpContext.User.Identity.Name)).ToList();
            
            return View(viewModel);
        }

    }
}
