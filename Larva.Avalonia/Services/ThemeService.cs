using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Themes.Fluent;
using Larva.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Services;

public sealed class ThemeService
{
    private readonly string larvaPath = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        $"{nameof(Larva)}Theme");

    public async Task ToggleThemeAsync()
    {
        // Ask Avalonia, do not ask me.
        var view = App.Current.Services.GetRequiredService<ShellView>();
        var dimensions = (view.Height, view.Width);

        var theme = (FluentTheme) App.Current.Styles[4];
        theme.Mode = theme.Mode == FluentThemeMode.Dark ? FluentThemeMode.Light : FluentThemeMode.Dark;

        view.Height = dimensions.Height;
        view.Width = dimensions.Width;

        await File.WriteAllTextAsync(larvaPath, theme.Mode == FluentThemeMode.Dark ? "D" : "L");
    }

    public async Task UseSavedThemeAsync()
    {
        var theme = (FluentTheme) App.Current.Styles[4];

        if (!File.Exists(larvaPath))
        {
            theme.Mode = FluentThemeMode.Light;
            return;
        }

        var content = await File.ReadAllTextAsync(larvaPath);
        var isDark = content.Equals("D", StringComparison.OrdinalIgnoreCase);

        theme.Mode = isDark ? FluentThemeMode.Dark : FluentThemeMode.Light;
    }
}