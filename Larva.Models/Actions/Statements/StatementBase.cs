using System.Runtime.Serialization;

namespace Larva.Models.Actions.Statements;

[DataContract]
[KnownType(typeof(EqualsStatement))]
public abstract class StatementBase : ActionBase
{
    [DataMember]
    public ActionBase[]? Actions { get; init; }
}