using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Larva.Avalonia.ViewModels.Dialogs;

public sealed partial class ProjectCreateDialogViewModel : ObservableValidator
{
    public event EventHandler? Close;

    [ObservableProperty]
    [Required]
    private string name = string.Empty;

    [ObservableProperty]
    [Required]
    private string token = string.Empty;


    [ObservableProperty]
    private string description = string.Empty;

    [RelayCommand]
    private void Create()
    {
        if (HasErrors)
        {
            return;
        }

        Close?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
    {
        ValidateAllProperties();
        base.OnPropertyChanged(eventArgs);
    }
}