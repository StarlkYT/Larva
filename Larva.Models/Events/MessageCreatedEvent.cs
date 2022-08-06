using Larva.Models.Actions;

namespace Larva.Models.Events;

public sealed record MessageCreatedEvent(ActionBase[] Actions) : EventBase;