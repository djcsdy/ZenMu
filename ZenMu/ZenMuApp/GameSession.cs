using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using ZenMu.ZenMuApp.Messages;

namespace ZenMu.ZenMuApp
{
	public class GameSession
	{
		private List<Player> _participants;
		private List<Scene> _scenes;
        private Game Game { get; set; }
	    public Guid Id { get { return Game.Id; } }

		public string Name { get; set; }

		public GameSession(Game game)
		{
			_participants = new List<Player>();
			_scenes = new List<Scene> { new Scene("Default"), new Scene("OOC") };
		    Game = game;
		}

		public bool AddPlayer(Player player)
		{
			if (Game.Players.Contains(player.GetId()))
			{
				_participants.Add(player);
				player.MessageRecieved += OnMessageRecieved;
				player.NameChanged += OnNameChanged;
				return true;
			}
			return false;
		}

        public IEnumerable<Guid> GetPlayerIds()
        {
            return _participants.Select(p => p.Id);
        }

		public void RemovePlayer(Player player)
		{
			_participants.Remove(player);
		}

        public void SendMessage(Message message)
        {
            var wireMessage = JsonConvert.SerializeObject(message);
            _participants.ForEach(p => p.Send(wireMessage));
        }

		private void OnMessageRecieved(Player player, string wireMessage)
		{
			SendMessage(ProcessMessage(player, wireMessage));
		}

		private void OnNameChanged(Player player, string newName)
		{
			
		}

		private Message ProcessMessage(Player player, string input)
		{
		    var message = JsonConvert.DeserializeObject<Message>(input);
            using (var db = MvcApplication.Store.OpenSession())
            {
                db.Store(message);
                db.SaveChanges();
            }
            Message output = MessageFactory.
		    return output;
		}
	}
}