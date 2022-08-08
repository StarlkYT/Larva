using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;

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
    [Required]
    private string path = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    private readonly FolderDialogService folderDialogService;
    private readonly ProjectService projectService;

    public ProjectCreateDialogViewModel(FolderDialogService folderDialogService,
        ProjectService projectService)
    {
        this.folderDialogService = folderDialogService;
        this.projectService = projectService;
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            return;
        }

        Close?.Invoke(this, EventArgs.Empty);
        await projectService.CreateAsync(new Project(Name, Token, Path, Description, null));
    }

    [RelayCommand]
    private async Task SelectFolder()
    {
        Path = await folderDialogService.ShowAsync() ?? string.Empty;
    }
}