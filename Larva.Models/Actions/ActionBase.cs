using System.Runtime.Serialization;
using Larva.Models.Actions.Statements;

namespace Larva.Models.Actions;

[DataContract]
[KnownType(typeof(MessageResponseAction))]
[KnownType(typeof(StatementBase))]
public abstract class ActionBase
{
    [DataMember(IsRequired = true)]
    public string Name { get; init; } = null!;
}