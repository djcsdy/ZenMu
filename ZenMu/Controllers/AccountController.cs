using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZenMu.Models;
using ZenMu.Utilities;


namespace ZenMu.Controllers
{
    public class AccountController : RavenController
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
		public ActionResult LogOn(string username, string password, bool persistant = false)
		{
            if (IsValidUser(username, password))
            {
                var authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddDays(1), persistant, "");
                string encrypedTicket = FormsAuthentication.Encrypt(authTicket);
                
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypedTicket);
                Response.Cookies.Add(authCookie);
                
                return new RedirectResult(FormsAuthentication.GetRedirectUrl(username, false));
            }
		    return View();
		}

		public ActionResult Register(string username, string password)
		{
		    var user = new ZenMuUser
		                   {
		                       Username = username, 
                               Password = BCrypt.HashPassword(password, BCrypt.GenerateSalt())
		                   };
		    RavenSession.Store(user);
            RavenSession.SaveChanges();

		    return RedirectToAction("Index", "Home");
		}

		private bool IsValidUser(string username, string password)
		{
		    var user = RavenSession.Query<ZenMuUser>().SingleOrDefault(u => u.Username == username);
            return user != null && BCrypt.CheckPassword(password, user.Password);
		}

    }
}
