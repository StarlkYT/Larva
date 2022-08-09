using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Larva.Avalonia.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Views.Dialogs;

public partial class RunDialogView : Window
{
    public RunDialogView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}