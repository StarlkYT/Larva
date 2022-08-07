using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Larva.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Views;

public sealed partial class ShellView : Window
{
    public ShellView()
    {
        DataContext = App.Current.Services.GetRequiredService<ShellViewModel>();
        InitializeComponent();
    }

    private void RectangleOnPointerEnter(object? sender, PointerEventArgs eventArgs)
    {
        ProjectMenuItem.Background = (IBrush?) App.Current?.FindResource("MenuFlyoutItemBackgroundPointerOver");
    }

    private void RectangleOnPointerLeave(object? sender, PointerEventArgs eventArgs)
    {
        ProjectMenuItem.Background = (IBrush?) App.Current?.FindResource("MenuFlyoutItemBackground");
    }

    private void RectangleOnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        ProjectMenuItem.IsSubMenuOpen = true;
    }
}