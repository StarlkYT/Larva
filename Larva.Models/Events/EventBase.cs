using System.Runtime.Serialization;
using Larva.Models.Actions;

namespace Larva.Models.Events;

[DataContract]
[KnownType(typeof(MemberJoinEvent))]
[KnownType(typeof(ChannelCreateEvent))]
public abstract class EventBase
{
    [DataMember]
    public ActionBase[]? Actions { get; set; }
}