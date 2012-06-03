using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;

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

		public void RemovePlayer(Player player)
		{
			_participants.Remove(player);
		}

		private void OnMessageRecieved(Player player, string message)
		{
			_participants.ForEach(p => p.Send(ProcessMessage(player, message)));
		}

		private void OnNameChanged(Player player, string newName)
		{
			
		}

		private string ProcessMessage(Player player, string input)
		{
		    var message = JsonConvert.DeserializeObject<Message>(input);
            using (var db = MvcApplication.Store.OpenSession())
            {
                db.Store(message);
                db.SaveChanges();
            }
			return input;
		}
	}
}