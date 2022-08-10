using System.Runtime.Serialization;
using Larva.Models;

namespace Larva.Avalonia.Models;

[DataContract]
public sealed class Project
{
    [DataMember(IsRequired = true)]
    public string Name { get; init; } = null!;

    [DataMember(IsRequired = true)]
    public string Token { get; init; } = null!;

    [DataMember(IsRequired = true)]
    public string Path { get; init; } = null!;

    [DataMember(IsRequired = true)]
    public string Description { get; init; } = null!;

    [DataMember]
    public Root? Root { get; init; }

    public bool IsValid()
    {
        return !(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Token)
                                                 || string.IsNullOrWhiteSpace(Path));
    }
}