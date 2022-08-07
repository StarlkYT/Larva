using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Larva.Avalonia.Models;
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
        viewModel.Close += (_, _) => Close(new Project(viewModel.Name, viewModel.Description, viewModel.Token, null));

        AvaloniaXamlLoader.Load(this);
    }
}