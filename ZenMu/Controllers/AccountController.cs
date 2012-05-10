using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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

		public ActionResult Login(string email, string password)
		{
			
		}

		public ActionResult Register(string email, string password)
		{
			
		}

		private bool IsValidUser(string username, string password)
		{
			RavenSession.
		}

    }
}
