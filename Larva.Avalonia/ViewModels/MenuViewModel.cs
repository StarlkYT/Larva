using System;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Larva.Avalonia.Services;

namespace Larva.Avalonia.ViewModels;

public sealed partial class MenuViewModel : ObservableObject
{
    private readonly ProjectCreateDialogService projectCreateDialogService;
    private readonly ThemeService themeService;

    public MenuViewModel(ProjectCreateDialogService projectCreateDialogService, ThemeService themeService)
    {
        this.projectCreateDialogService = projectCreateDialogService;
        this.themeService = themeService;
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        await projectCreateDialogService.ShowAsync();
    }

    [RelayCommand]
    private Task OpenAsync()
    {
        throw new NotImplementedException();
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
}