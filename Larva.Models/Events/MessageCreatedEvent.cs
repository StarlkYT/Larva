using Larva.Models.Actions;

namespace Larva.Models.Events;

public sealed class MessageCreatedEvent : EventBase
{
    public ActionBase[]? Actions { get; init; }
}