namespace ZenMu.ZenMuApp.Messages
{
    public enum CommandType
	{
		Message,
		Emote,
        PlayerJoin,
        PlayerQuit,
        NewCharacter,
        NameChange, 
        NewScene,
        RemoveScene,
        DiceRoll,
        DamageDiceRoll,
        TargetDiceRoll,
	}
}