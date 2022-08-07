using Larva.Models.Events;

namespace Larva.Models;

public sealed class Root
{
    public EventBase[]? Events { get; init; }
}