using System.Threading.Tasks;
using Larva.Avalonia.ViewModels.Dialogs;
using Larva.Avalonia.Views;
using Larva.Avalonia.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Services;

public sealed class MessageBoxDialogService
{
    public async Task ShowAsync(string title, string message)
    {
        var dialog = App.Current.Services.GetRequiredService<MessageBoxDialogView>();

        var dataContext = App.Current.Services.GetRequiredService<MessageBoxDialogViewModel>();

        dataContext.Title = title;
        dataContext.Message = message;

        dialog.DataContext = dataContext;
        await dialog.ShowDialog(App.Current.Services.GetRequiredService<ShellView>());
    }
}