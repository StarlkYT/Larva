using System.Threading.Tasks;
using Larva.Avalonia.Views;
using Larva.Avalonia.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Services;

public sealed class ProjectCreateDialogService
{
    public async Task ShowAsync()
    {
        var projectCreateDialogView = App.Current.Services.GetRequiredService<ProjectCreateDialogView>();
        await projectCreateDialogView.ShowDialog(App.Current.Services.GetRequiredService<ShellView>())!;
    }
}