using System.Runtime.Serialization;

namespace Larva.Avalonia.Models;

[DataContract]
public sealed class Recent
{
    [DataMember(IsRequired = true)]
    public string Name { get; init; } = null!;

    [DataMember(IsRequired = true)]
    public string Path { get; init; } = null!;
    
    public bool IsValid()
    {
        return !(string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Path));
    }
}