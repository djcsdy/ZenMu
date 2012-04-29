using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fleck;

namespace ZenMu.ZenMuApp
{
	public class Player
	{
		private IWebSocketConnection _socket;
		private GameSession _game;

		private string _characterName;
		public string CharacterName
		{
			get { return _characterName; }
			set 
			{ 
				_characterName = value;
				NameChanged(this, value);
			}
		}

		public List<string> AuthorizedNames; 

		public Player(IWebSocketConnection socket)
		{
			_socket = socket;
			_socket.OnMessage = message => MessageRecieved(this, message);
			_socket.OnClose += LeaveGame;

		}

		public void JoinGame(GameSession game)
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

		public event Action<Player, string> NameChanged;
		public event Action<Player, String> MessageRecieved;
	}
}