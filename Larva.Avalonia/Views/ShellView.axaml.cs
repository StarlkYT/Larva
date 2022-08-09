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

    private void ProjectRectangleOnPointerEnter(object? sender, PointerEventArgs eventArgs)
    {
        ProjectMenuItem.Background = (IBrush?) App.Current?.FindResource("MenuFlyoutItemBackgroundPointerOver");
    }

    private void ProjectRectangleOnPointerLeave(object? sender, PointerEventArgs eventArgs)
    {
        ProjectMenuItem.Background = (IBrush?) App.Current?.FindResource("MenuFlyoutItemBackground");
    }

    private void ProjectRectangleOnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        ProjectMenuItem.IsSubMenuOpen = true;
    }

    private void ViewRectangleOnPointerEnter(object? sender, PointerEventArgs eventArgs)
    {
        ViewMenuItem.Background = (IBrush?) App.Current?.FindResource("MenuFlyoutItemBackgroundPointerOver");
    }

    private void ViewRectangleOnPointerLeave(object? sender, PointerEventArgs eventArgs)
    {
        ViewMenuItem.Background = (IBrush?) App.Current?.FindResource("MenuFlyoutItemBackground");
    }

    private void ViewRectangleOnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        ViewMenuItem.IsSubMenuOpen = true;
    }
    
    private void RunRectangleOnPointerEnter(object? sender, PointerEventArgs eventArgs)
    {
        RunMenuItem.Background = (IBrush?) App.Current?.FindResource("MenuFlyoutItemBackgroundPointerOver");
    }

    private void RunRectangleOnPointerLeave(object? sender, PointerEventArgs eventArgs)
    {
        RunMenuItem.Background = (IBrush?) App.Current?.FindResource("MenuFlyoutItemBackground");
    }

    private void RunRectangleOnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        RunMenuItem.IsSubMenuOpen = true;
    }
}