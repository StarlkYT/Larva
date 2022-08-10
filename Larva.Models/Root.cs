using System.Runtime.Serialization;
using Larva.Models.Events;

namespace Larva.Models;

[DataContract]
public sealed class Root
{
    [DataMember]
    public EventBase[]? Events { get; init; }
}