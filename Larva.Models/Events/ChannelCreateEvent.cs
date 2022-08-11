using System.Runtime.Serialization;
using Larva.Models.Actions;

namespace Larva.Models.Events;

[DataContract]
public sealed class ChannelCreateEvent : EventBase
{
    [DataMember]
    public ActionBase[]? Actions { get; init; }
}