using System.Threading.Tasks;
using Larva.Avalonia.Models;
using Larva.Avalonia.ViewModels.Dialogs;
using Larva.Avalonia.Views;
using Larva.Avalonia.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Services;

public sealed class RunDialogService
{
    public async Task ShowAsync(Project project)
    {
        var dialog = App.Current.Services.GetRequiredService<RunDialogView>();

        var dataContext = App.Current.Services.GetRequiredService<RunDialogViewModel>();

        dataContext.Project = project;

        dialog.DataContext = dataContext;
        await dialog.ShowDialog(App.Current.Services.GetRequiredService<ShellView>());
    }
}