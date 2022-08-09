using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;
using Microsoft.Extensions.Logging;

namespace Larva.Avalonia.ViewModels;

public sealed partial class MenuViewModel : ObservableObject
{
    [ObservableProperty]
    private Project? currentProject;

    private readonly ILogger<MenuViewModel> logger;
    private readonly ProjectCreateDialogService projectCreateDialogService;
    private readonly ThemeService themeService;
    private readonly ProjectService projectService;
    private readonly MessageBoxDialogService messageBoxDialogService;
    private readonly FileDialogService fileDialogService;
    private readonly RunDialogService runDialogService;

    public MenuViewModel(ILogger<MenuViewModel> logger, ProjectCreateDialogService projectCreateDialogService,
        ThemeService themeService,
        ProjectService projectService, MessageBoxDialogService messageBoxDialogService,
        FileDialogService fileDialogService, RunDialogService runDialogService)
    {
        this.logger = logger;
        this.projectCreateDialogService = projectCreateDialogService;
        this.themeService = themeService;
        this.projectService = projectService;
        this.messageBoxDialogService = messageBoxDialogService;
        this.fileDialogService = fileDialogService;
        this.runDialogService = runDialogService;
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        logger.LogInformation("Opened create project dialog");
        await projectCreateDialogService.ShowAsync();
    }

    [RelayCommand]
    private async Task OpenAsync()
    {
        logger.LogInformation("Opened file dialog to select a project");
        var path = await fileDialogService.ShowAsync("Open Project");

        if (path is not null)
        {
            var result = await projectService.OpenAsync(path);

            if (result.IsFailed)
            {
                logger.LogError("Could not open the project '{Error}'", result.Errors[0].Message);

                await messageBoxDialogService.ShowAsync("Catastrophic Failure", result.Errors[0].Message);
            }
        }
    }

    [RelayCommand]
    private void Exit()
    {
        logger.LogInformation("Closed the application");

        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.TryShutdown();
        }
    }

    [RelayCommand]
    private async Task ToggleThemeAsync()
    {
        logger.LogInformation("Toggled the theme");

        await themeService.ToggleThemeAsync();
    }

    [RelayCommand]
    private async Task RunAsync()
    {
        logger.LogInformation("Running a project");

        await runDialogService.ShowAsync(CurrentProject!);
    }

    [RelayCommand]
    private void OpenLogs()
    {
        logger.LogInformation("Opened logs");

        var path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            $"{nameof(Larva)}Logs.txt");
        
        new Process()
        {
            StartInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true
            }
        }.Start();
    }
}