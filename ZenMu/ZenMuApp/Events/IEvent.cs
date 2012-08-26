using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZenMu.ZenMuApp.Events
{
    public interface IEvent
    {
        EventType Type { get; }
        DateTime CreateDate { get; set; }
        Guid? GameId { get; set; }
        Guid Source { get; set; }
    }
}
