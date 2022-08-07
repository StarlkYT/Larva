using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Message;
using Larva.Avalonia.Models;

namespace Larva.Avalonia.ViewModels;

public sealed partial class EditorViewModel : ObservableObject
{
    [ObservableProperty]
    private Project? project;

    public EditorViewModel()
    {
        WeakReferenceMessenger.Default.Register<ProjectCreateMessage>(this, (_, message) => Project = message.Project);
    }
}