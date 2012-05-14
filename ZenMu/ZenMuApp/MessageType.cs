using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenMu.ZenMuApp
{
    public enum MessageType
	{
		Message,
		Emote,
        PlayerJoin,
        PlayerQuit,
        NewCharacter,
        NameChange, 
        NewScene,
        RemoveScene,
        ErrorMessage
	}
}