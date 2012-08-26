using System;

namespace ZenMu.ZenMuApp.Events
{
    public class MessageEvent : IEvent
    {
        public EventType Type { get { return EventType.Message; } }
        public DateTime CreateDate { get; set; }
        public Guid? GameId { get; set; }
        public Guid Source { get; set; }
        public string CharacterName;
        public Guid SceneId;
        public string Message;
    }
}