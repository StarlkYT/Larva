using CommunityToolkit.Mvvm.ComponentModel;

namespace Larva.Avalonia.ViewModels.Dialogs;

public sealed partial class MessageBoxDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string title = "Larva";

    [ObservableProperty]
    private string? primaryButton;

    [ObservableProperty]
    private string? message;
}