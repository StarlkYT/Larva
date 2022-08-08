using Larva.Models;

namespace Larva.Avalonia.Models;

public sealed record Project(string Name, string Token, string Path, string Description, Root? Root);