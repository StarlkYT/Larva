using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Message;

namespace Larva.Avalonia.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private string? projectName;
    
    public MenuViewModel MenuViewModel { get; }

    public ShellViewModel(MenuViewModel menuViewModel)
    {
        MenuViewModel = menuViewModel;

        WeakReferenceMessenger.Default.Register<ProjectCreateMessage>(this,
            (_, message) => ProjectName = message.Project.Name);
    }
}