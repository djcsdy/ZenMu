using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fleck;

namespace ZenMu.ZenMuApp
{
	public static class Storyteller
	{
		private static List<ZenMuSession> _games = new List<ZenMuSession>();

		public static void StartServer()
		{
			var server = new WebSocketServer("ws://localhost:25948");
			server.Start(ws =>
			             	{
			             		ws.OnOpen = () =>
			             		            	{
			             		            		if (_games.Count == 0)
			             		            		{
			             		            			_games.Add(new ZenMuSession());
			             		            		}
			             		            		_games.First().AddPlayer(new ZenMuPlayer(ws));
			             		            	};

			             	});
		}
	}
}