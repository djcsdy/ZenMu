using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Fleck;
using ZenMu.Auth;

namespace ZenMu.ZenMuApp
{
	public class Storyteller
	{
		private List<GameSession> _games = new List<GameSession>();

		public void CreateGame(string gameName)
		{
            using (var db = MvcApplication.Store.OpenSession())
            {
                var newGame = new GameSession(db.Query<Game>().Single(g => g.Name == gameName));
                _games.Add(newGame);
            }
		}

		public void StartServer()
		{
			var server = new WebSocketServer("ws://localhost:25948");
			server.Start(ws =>
			                 {
                                 ws.OnOpen = () => OnOpenConnection(ws); 
			             	});
		}

        private void OnOpenConnection(IWebSocketConnection socket)
        {
            var authCookie = socket.ConnectionInfo.Cookies[FormsAuthentication.FormsCookieName];
            
            if (authCookie == null) 
            { 
                socket.Close();
                return;
            }

            var authTicket = FormsAuthentication.Decrypt(authCookie);
            var identity = new ZenMuIdentity(authTicket.Name);
            var principal = new ZenMuPrincipal(identity);
            var player = new Player(socket, principal);
        }
	}
}