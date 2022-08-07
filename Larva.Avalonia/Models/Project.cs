using Larva.Models;

namespace Larva.Avalonia.Models;

public sealed record Project(string Name, string Description, string Token, Root? Root);