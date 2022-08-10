using System.Runtime.Serialization;

namespace Larva.Models.Actions;

[DataContract]
public sealed class MessageResponseAction : ActionBase
{
    [DataMember(IsRequired = true)]
    public string Literal { get; init; } = null!;
}