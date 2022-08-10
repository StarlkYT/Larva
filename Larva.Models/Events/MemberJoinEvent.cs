using System.Runtime.Serialization;
using Larva.Models.Actions;

namespace Larva.Models.Events;

[DataContract]
public sealed class MemberJoinEvent : EventBase
{
    [DataMember]
    public ActionBase[]? Actions { get; init; }
}