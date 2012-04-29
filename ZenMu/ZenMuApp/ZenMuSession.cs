using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenMu.ZenMuApp
{
	public class ZenMuSession
	{
		private List<ZenMuPlayer> _participants;

		public ZenMuSession()
		{
			_participants = new List<ZenMuPlayer>();
		}

		public void AddPlayer(ZenMuPlayer player)
		{
			_participants.Add(player);
			player.MessageRecieved += OnMessageRecieved;
		}

		public void RemovePlayer(ZenMuPlayer player)
		{
			_participants.Remove(player);
		}

		private void OnMessageRecieved(ZenMuPlayer player, string message)
		{
			_participants.ForEach(p => p.Send(ProcessMessage(message)));
		}

		private string ProcessMessage(string input)
		{
			return input;
		}
	}
}