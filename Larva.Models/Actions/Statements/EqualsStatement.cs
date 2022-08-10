﻿using System.Runtime.Serialization;

namespace Larva.Models.Actions.Statements;

[DataContract]
public sealed class EqualsStatement : ActionBase
{
    [DataMember(IsRequired = true)]
    public string Literal { get; init; } = null!;

    public ActionBase[]? Actions { get; init; }
}