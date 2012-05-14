using System;
using System.Security.Principal;
using Fleck;

namespace ZenMu.ZenMuApp
{
	public class Player
	{
		private IWebSocketConnection _socket;
		private GameSession _game;
		private string _characterName;
		public bool IsStoryteller { get; private set; }
        public IPrincipal Identity { get; private set; }

		public string CharacterName
		{
			get { return _characterName; }
			set 
			{ 
				_characterName = value;
				NameChanged(this, value);
			}
		}

		public Player(IWebSocketConnection socket, IPrincipal principal)
		{
		    Identity = principal;
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