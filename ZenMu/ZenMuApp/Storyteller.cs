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

		public void CreateGame(Guid gameId)
		{
            using (var db = MvcApplication.Store.OpenSession())
            {
                var newGame = new GameSession(db.Query<Game>().Single(g => g.Id == gameId));
                _games.Add(newGame);
            }
		}

        public IEnumerable<Guid> GamesContainingPlayer(Guid playerId)
        {
            return _games.Where(g => g.GetPlayerIds().Contains(playerId)).Select(g => g.Id);
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

            Player player;

            if (authCookie == null || !TryCreatePlayer(authCookie, socket, out player)) 
            { 
                socket.Close();
                return;
            }

            Guid gameGuid;

            if (Guid.TryParse(socket.ConnectionInfo.Path.Replace("", String.Empty), out gameGuid))
            {
                if (_games.Any(g => g.Id == gameGuid))
                {
                    _games.Single(g => g.Id == gameGuid).AddPlayer(player);
                }
            }
        }

        private bool TryCreatePlayer(string authCookie, IWebSocketConnection socket, out Player result)
        {
            try
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie);
                var identity = new ZenMuIdentity(authTicket.Name);
                var principal = new ZenMuPrincipal(identity);
                result = new Player(socket, principal);
            }
            catch(Exception e)
            {
                result = null;
                return false;
            }
            return true;
        }
	}
}