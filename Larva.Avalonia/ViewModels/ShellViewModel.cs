using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Message;
using Larva.Avalonia.Models;

namespace Larva.Avalonia.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private Project? currentProject;

    public MenuViewModel MenuViewModel { get; }

    public ShellViewModel(MenuViewModel menuViewModel)
    {
        MenuViewModel = menuViewModel;

        WeakReferenceMessenger.Default.Register<ProjectCreateMessage>(this,
            (_, message) => CurrentProject = message.Project);
    }
}