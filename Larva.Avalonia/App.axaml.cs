using System;
using System.IO;
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
using Microsoft.Extensions.Logging;
using Serilog;

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
            .AddTransient<RunDialogViewModel>()
            .AddTransient<RunDialogView>()
            .AddTransient<RunDialogService>()
            .AddTransient<DiscordClientService>()
            .AddTransient<EventsViewModel>()
            .AddSingleton<DiscordRichPresenceService>()
            .AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                
                var logger = new LoggerConfiguration()
                    .WriteTo.File(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        $"{nameof(Larva)}Logs.txt"));
                
                builder.AddSerilog(logger.CreateLogger());
            })
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