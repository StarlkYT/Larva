using System.Runtime.Serialization;

namespace Larva.Models.Events;

[DataContract]
[KnownType(typeof(MemberJoinEvent))]
public abstract class EventBase
{
}