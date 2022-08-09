using Larva.Models;
using Newtonsoft.Json;

namespace Larva.Avalonia.Models;

public sealed class Project
{
    [JsonProperty(Required = Required.Always)]
    public string Name { get; init; } = null!;

    [JsonProperty(Required = Required.Always)]
    public string Token { get; init; } = null!;

    [JsonProperty(Required = Required.Always)]
    public string Path { get; init; } = null!;

    [JsonProperty(Required = Required.Always)]
    public string Description { get; init; } = null!;

    public Root? Root { get; init; }
}