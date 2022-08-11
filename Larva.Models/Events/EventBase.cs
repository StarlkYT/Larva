using System.Runtime.Serialization;

namespace Larva.Models.Events;

[DataContract]
[KnownType(typeof(MemberJoinEvent))]
[KnownType(typeof(ChannelCreateEvent))]
public abstract class EventBase
{
}