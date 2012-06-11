using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Raven.Client.Document;
using Raven.Client.Indexes;
using ZenMu.Auth;
using ZenMu.Models;
using ZenMu.Utilities;
using ZenMu.ZenMuApp;

namespace ZenMu
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static DocumentStore Store;
	    public static Storyteller Storyteller;

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

		    Storyteller = new Storyteller();
            Storyteller.StartServer();

			Store = new DocumentStore {ConnectionStringName = "RavenDB"};
			Store.Initialize();
            SetUpInitialUsers();

			IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);
            
		}

        private static void SetUpInitialUsers()
        {
            using(var s = Store.OpenSession())
		    {
		        if (!s.Query<ZenMuUser>().Any())
		        {
		            var adminUser = new ZenMuUser
		                                {
                                            Id = new Guid(),
		                                    Username = "Administrator",
		                                    Password = BCrypt.HashPassword("ChangeMe", BCrypt.GenerateSalt()),
                                            Roles = new [] { "Administrator" }
		                                };
		            var systemUser = new ZenMuUser
		                                 {
		                                     Id = new Guid(),
		                                     Username = "System",
		                                     Password = BCrypt.HashPassword(new Guid().ToString(), BCrypt.GenerateSalt())
		                                 };
                    s.Store(adminUser);
                    s.Store(systemUser);
                    s.SaveChanges();
		        }
		    }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            
            if (authCookie == null) return;
            
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var identity = new ZenMuIdentity(authTicket.Name);
            var principal = new ZenMuPrincipal(identity);
            Context.User = principal;
            Thread.CurrentPrincipal = principal;
        }
	}
}