using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Messages;
using Larva.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Views;

public sealed partial class ShellView : Window
{
    private bool isFromShell;
    
    public ShellView()
    {
        DataContext = App.Current.Services.GetRequiredService<ShellViewModel>();
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<ConfirmShellCloseMessage>(this, (_, _) =>
        {
            isFromShell = true;
            Close();
        });
        
        Closing += (_, eventArgs) =>
        {
            if (!isFromShell)
            {
                eventArgs.Cancel = true;
                WeakReferenceMessenger.Default.Send<ProjectSaveMessage>();
            }
        };
    }

    private void ProjectRectangleOnPointerEnter(object? sender, PointerEventArgs eventArgs)
    {
        ProjectMenuItem.Background = (IBrush?) App.Current.FindResource("MenuFlyoutItemBackgroundPointerOver");
    }

    private void ProjectRectangleOnPointerLeave(object? sender, PointerEventArgs eventArgs)
    {
        ProjectMenuItem.Background = (IBrush?) App.Current.FindResource("MenuFlyoutItemBackground");
    }

    private void ProjectRectangleOnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        ProjectMenuItem.IsSubMenuOpen = true;
    }

    private void ViewRectangleOnPointerEnter(object? sender, PointerEventArgs eventArgs)
    {
        ViewMenuItem.Background = (IBrush?) App.Current.FindResource("MenuFlyoutItemBackgroundPointerOver");
    }

    private void ViewRectangleOnPointerLeave(object? sender, PointerEventArgs eventArgs)
    {
        ViewMenuItem.Background = (IBrush?) App.Current.FindResource("MenuFlyoutItemBackground");
    }

    private void ViewRectangleOnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        ViewMenuItem.IsSubMenuOpen = true;
    }
    
    private void RunRectangleOnPointerEnter(object? sender, PointerEventArgs eventArgs)
    {
        RunMenuItem.Background = (IBrush?) App.Current.FindResource("MenuFlyoutItemBackgroundPointerOver");
    }

    private void RunRectangleOnPointerLeave(object? sender, PointerEventArgs eventArgs)
    {
        RunMenuItem.Background = (IBrush?) App.Current.FindResource("MenuFlyoutItemBackground");
    }

    private void RunRectangleOnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        RunMenuItem.IsSubMenuOpen = true;
    }
}