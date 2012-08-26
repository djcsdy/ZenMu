using System;
using System.Linq;
using System.Security.Principal;
using Fleck;
using ZenMu.Models;
using ZenMu.ViewModels;

namespace ZenMu.ZenMuApp
{
	public class Player
	{
		private IWebSocketConnection _socket;
		private GameSession _game;
		private string _characterName;
		public bool IsStoryteller { get; private set; }
        public IPrincipal Principal { get; private set; }
        public Guid Id { get; private set; }

        public Player()
        {
            using (var db = MvcApplication.Store.OpenSession())
            {
                Id = db.Query<ZenMuUser>().Single(u => u.Username == Principal.Identity.Name).Id;
            }
        }

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
		    Principal = principal;
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