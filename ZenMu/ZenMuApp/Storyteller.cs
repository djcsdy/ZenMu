using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fleck;

namespace ZenMu.ZenMuApp
{
	public static class Storyteller
	{
		private static List<GameSession> _games = new List<GameSession>();

		public static void CreateGame(string name, string password, string storytellerKey)
		{
			var newGame = new GameSession(name, password, storytellerKey);
		}

		public static void StartServer()
		{
			var server = new WebSocketServer("ws://localhost:25948");
			server.Start(ws =>
			             	{
			             		ws.OnOpen = () =>
			             		            	{
			             		            		if (_games.Count == 0)
			             		            		{
			             		            			_games.Add(new GameSession());
			             		            		}
			             		            		_games.First().AddPlayer(new Player(ws));
			             		            	};

			             	});
		}
	}
}