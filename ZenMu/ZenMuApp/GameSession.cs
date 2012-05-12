using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenMu.ZenMuApp
{
	public class GameSession
	{
		private List<Player> _participants;
		private List<Scene> _scenes;
		public string Password { private get; set; }
		public string StorytellerKey { private get; set; }
		public string GameKey { get; private set; }

		public string Name { get; set; }

		public GameSession()
		{
			_participants = new List<Player>();
		}

		public GameSession(string name, string password, string storytellerKey)
		{
			_participants = new List<Player>();
			_scenes = new List<Scene>(){new Scene("Default")};
			Name = name;
			Password = password;
			StorytellerKey = storytellerKey;
		}

		public bool AddPlayer(Player player, string password)
		{
			if (password == Password)
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
			return input;
		}
	}
}