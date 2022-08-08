using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Larva.Avalonia.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Views.Dialogs;

public sealed partial class ProjectCreateDialogView : Window
{
    public ProjectCreateDialogView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        var viewModel = App.Current.Services.GetRequiredService<ProjectCreateDialogViewModel>();

        DataContext = viewModel;
        viewModel.Close += (_, _) => Close();

        AvaloniaXamlLoader.Load(this);
    }
}