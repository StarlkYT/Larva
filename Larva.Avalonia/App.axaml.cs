using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Larva.Avalonia.Services;
using Larva.Avalonia.ViewModels;
using Larva.Avalonia.ViewModels.Dialogs;
using Larva.Avalonia.Views;
using Larva.Avalonia.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia;

public sealed class App : Application
{
    public new static App Current => (App) Application.Current!;

    public IServiceProvider Services { get; }

    public App()
    {
        Services = new ServiceCollection()
            .AddSingleton<ShellView>()
            .AddTransient<ShellViewModel>()
            .AddTransient<MenuViewModel>()
            .AddTransient<ProjectCreateDialogService>()
            .AddTransient<ProjectCreateDialogView>()
            .AddTransient<ProjectCreateDialogViewModel>()
            .AddTransient<EditorViewModel>()
            .AddTransient<RecentProjectService>()
            .AddTransient<FolderDialogService>()
            .AddTransient<ProjectService>()
            .AddTransient<ThemeService>()
            .AddTransient<MessageBoxDialogViewModel>()
            .AddTransient<MessageBoxDialogView>()
            .AddTransient<MessageBoxDialogService>()
            .AddTransient<FileDialogService>()
            .BuildServiceProvider();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        ExpressionObserver.DataValidators.RemoveAll(plugin => plugin is DataAnnotationsValidationPlugin);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<ShellView>();
        }

        base.OnFrameworkInitializationCompleted();

        await Services.GetRequiredService<ThemeService>().UseSavedThemeAsync();
    }
}