using System.Runtime.Serialization;

namespace Larva.Models.Actions;

[DataContract]
[KnownType(typeof(MessageResponseAction))]
public abstract class ActionBase
{
    [DataMember(IsRequired = true)]
    public string Name { get; init; } = null!;
}