using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fleck;
using ZenMu.ZenMuApp;

namespace ZenMu
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
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

			Storyteller.StartServer();

			/*
			Application.Add("connections", new List<IWebSocketConnection>());
			var server = new WebSocketServer("ws://localhost:25948");
			server.Start(ws =>
			             	{
			             		ws.OnOpen = () =>
			             		            	{
			             		            		((List<IWebSocketConnection>) Application["connections"]).Add(ws);
			             		            		ws.Send((((List<IWebSocketConnection>) Application["connections"]).IndexOf(ws).ToString() + " connected at " + DateTime.Now.ToString()));
			             		            	};
								ws.OnMessage = message => ((List<IWebSocketConnection>)Application["connections"]).ForEach(s => s.Send((((List<IWebSocketConnection>)Application["connections"]).IndexOf(ws)).ToString() + " said: " + Server.HtmlEncode(message)));
							}
				);*/
		}
	}
}