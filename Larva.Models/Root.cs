using Larva.Models.Events;

namespace Larva.Models;

public sealed record Root(EventBase[] Events);