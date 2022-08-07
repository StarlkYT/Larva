using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Message;
using Larva.Avalonia.Models;

namespace Larva.Avalonia.ViewModels.Dialogs;

public sealed partial class ProjectCreateDialogViewModel : ObservableValidator
{
    public event EventHandler? Close;

    public Project? Project { get; private set; }

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
        ValidateAllProperties();

        if (HasErrors)
        {
            return;
        }

        Close?.Invoke(this, EventArgs.Empty);

        Project = new Project(Name, Token, Description, null);
        WeakReferenceMessenger.Default.Send(new ProjectCreateMessage(Project));
    }
}