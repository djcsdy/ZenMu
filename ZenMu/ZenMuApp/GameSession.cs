using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ZenMu.ZenMuApp
{
	public class GameSession
	{
		private List<Player> _participants;
		public string Password { private get; set; }
		public string StorytellerKey { private get; set; }

		public string Name { get; set; }

		public GameSession()
		{
			_participants = new List<Player>();
		}

		public GameSession(string name, string password, string storytellerKey)
		{
			_participants = new List<Player>();
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

		private string ProcessMessage(Player player, string input)
		{
			var message = JsonConvert.DeserializeObject<ZenMuMessage>(input);
			string output;
			switch (message.MessageType)
			{
				case MessageType.Message:
				case MessageType.Emote:
					if(player.)
					break;
				case MessageType.Command:
					break;
			}
			return input;
		}
	}
}