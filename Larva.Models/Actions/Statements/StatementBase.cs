using System.Runtime.Serialization;

namespace Larva.Models.Actions.Statements;

[DataContract]
[KnownType(typeof(EqualsStatement))]
public abstract class StatementBase
{
    [DataMember(IsRequired = true)]
    public string Name { get; init; } = null!;
}