using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Larva.Avalonia.Views.Dialogs;

public sealed partial class RunDialogView : Window
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