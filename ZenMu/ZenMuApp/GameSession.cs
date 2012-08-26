using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using ZenMu.ZenMuApp.Events;

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

        public void EndSession()
        {
            foreach (Player player in _participants)
            {
                player.LeaveGame();
            }
        }

		public bool AddPlayer(Player player)
		{
			if (Game.Players.Contains(player.Id))
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

        public void SendMessage(EventWrapper wrappedEvent)
        {
            var wireMessage = JsonConvert.SerializeObject(wrappedEvent);
            _participants.ForEach(p => p.Send(wireMessage));
        }

		private void OnMessageRecieved(Player player, string wireMessage)
		{
		    ProcessMessage(player, wireMessage);
		}

		private void OnNameChanged(Player player, string newName)
		{
			
		}

        public bool SceneIsActive(Guid sceneId)
        {
            return _scenes.Exists(s => s.Id == sceneId) && _scenes.Single(s => s.Id == sceneId).IsActive;
        }

		private void ProcessMessage(Player player, string input)
		{
		    var receivedEvent = JsonConvert.DeserializeObject<EventWrapper>(input);

            using (var db = MvcApplication.Store.OpenSession())
            {
                switch (receivedEvent.EventType)
                {
                    case EventType.Message:
                        var message = JsonConvert.DeserializeObject<MessageEvent>(receivedEvent.EventBody);
                        if (SceneIsActive(message.SceneId))
                        {
                            message.GameId = Game.Id;
                            SendMessage(receivedEvent);
                        }
                        break;
                }

                db.SaveChanges();
            }
		}
	}
}