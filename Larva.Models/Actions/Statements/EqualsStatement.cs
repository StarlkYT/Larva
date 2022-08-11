using System.Runtime.Serialization;

namespace Larva.Models.Actions.Statements;

[DataContract]
public sealed class EqualsStatement : StatementBase
{
    [DataMember(IsRequired = true)]
    public string Literal { get; init; } = null!;
}