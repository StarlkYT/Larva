using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Larva.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Views;

public sealed partial class EditorView : UserControl
{
    public EditorView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        DataContext = App.Current.Services.GetRequiredService<EditorViewModel>();
        AvaloniaXamlLoader.Load(this);
    }
}