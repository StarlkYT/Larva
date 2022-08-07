using CommunityToolkit.Mvvm.ComponentModel;

namespace Larva.Avalonia.ViewModels;

public sealed class ShellViewModel : ObservableObject
{
    public MenuViewModel MenuViewModel { get; }

    public ShellViewModel(MenuViewModel menuViewModel)
    {
        MenuViewModel = menuViewModel;
    }
}