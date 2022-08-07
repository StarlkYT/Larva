namespace Larva.Models.Actions.Statements;

public sealed class EqualsStatement : ActionBase
{
    public string? Literal { get; init; }

    public ActionBase[]? Actions { get; init; }
}