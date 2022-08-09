using System;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;

namespace Larva.Avalonia.ViewModels;

public sealed partial class MenuViewModel : ObservableObject
{
    [ObservableProperty]
    private Project? currentProject;
    
    private readonly ProjectCreateDialogService projectCreateDialogService;
    private readonly ThemeService themeService;
    private readonly ProjectService projectService;
    private readonly MessageBoxDialogService messageBoxDialogService;
    private readonly FileDialogService fileDialogService;

    public MenuViewModel(ProjectCreateDialogService projectCreateDialogService, ThemeService themeService,
        ProjectService projectService, MessageBoxDialogService messageBoxDialogService,
        FileDialogService fileDialogService)
    {
        this.projectCreateDialogService = projectCreateDialogService;
        this.themeService = themeService;
        this.projectService = projectService;
        this.messageBoxDialogService = messageBoxDialogService;
        this.fileDialogService = fileDialogService;
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        await projectCreateDialogService.ShowAsync();
    }

    [RelayCommand]
    private async Task OpenAsync()
    {
        var path = await fileDialogService.ShowAsync("Open Project");

        if (path is not null)
        {
            var result = await projectService.OpenAsync(path);

            if (result.IsFailed)
            {
                await messageBoxDialogService.ShowAsync("Catastrophic Failure", result.Errors[0].Message);
            }
        }
    }

    [RelayCommand]
    private void Exit()
    {
        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.TryShutdown();
        }
    }

    [RelayCommand]
    private async Task ToggleThemeAsync()
    {
        await themeService.ToggleThemeAsync();
    }

    [RelayCommand]
    private Task RunAsync()
    {
        throw new NotImplementedException();
    }
    
    [RelayCommand]
    private Task ForceStopAsync()
    {
        throw new NotImplementedException();
    }
    
    [RelayCommand]
    private Task LogsAsync()
    {
        throw new NotImplementedException();
    }
}