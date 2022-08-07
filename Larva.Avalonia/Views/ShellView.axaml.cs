using Avalonia.Controls;
using Larva.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Views;

public sealed partial class ShellView : Window
{
    public ShellView()
    {
        DataContext = App.Current?.Services.GetRequiredService<ShellViewModel>();
        InitializeComponent();
    }
}