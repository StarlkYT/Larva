using System.Runtime.Serialization;

namespace Larva.Models.Events;

[DataContract]
public sealed class MemberJoinEvent : EventBase
{
}