using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Larva.Avalonia.ViewModels;
using Larva.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia;

public sealed class App : Application
{
    public new static App? Current => (App?) Application.Current;

    public IServiceProvider Services { get; }

    public App()
    {
        Services = new ServiceCollection()
            .AddSingleton<ShellView>()
            .AddTransient<ShellViewModel>()
            .BuildServiceProvider();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<ShellView>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}