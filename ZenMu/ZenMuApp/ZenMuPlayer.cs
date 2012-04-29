using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fleck;

namespace ZenMu.ZenMuApp
{
	public class ZenMuPlayer
	{
		private IWebSocketConnection _socket;
		private ZenMuSession _game;

		public ZenMuPlayer(IWebSocketConnection socket)
		{
			_socket = socket;
			_socket.OnMessage = message => MessageRecieved(this, message);
			//_socket.OnClose = 
		}

		public void JoinGame(ZenMuSession game)
		{
			_game = game;
		}

		public void Send(string message)
		{
			_socket.Send(message);
		}

		public void LeaveGame()
		{
			_game.RemovePlayer(this);
			_game = null;
		}
		
		public event Action<ZenMuPlayer, String> MessageRecieved;
		
	}
}