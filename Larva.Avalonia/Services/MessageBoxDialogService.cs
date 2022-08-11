using System.Threading.Tasks;
using Avalonia.Controls;
using Larva.Avalonia.ViewModels.Dialogs;
using Larva.Avalonia.Views;
using Larva.Avalonia.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Services;

public sealed class MessageBoxDialogService
{
    public async Task<bool?> ShowAsync(string title, string message, string? primaryButton = null, Window? parent = null)
    {
        var dialog = App.Current.Services.GetRequiredService<MessageBoxDialogView>();

        var dataContext = App.Current.Services.GetRequiredService<MessageBoxDialogViewModel>();

        dataContext.Title = title;
        dataContext.Message = message;
        dataContext.PrimaryButton = primaryButton;
        
        dialog.DataContext = dataContext;

        if (parent is not null)
        {
            if (primaryButton is not null)
            {
                return await dialog.ShowDialog<bool>(parent);
            }
            
            await dialog.ShowDialog(parent);
        }
        else
        {
            if (primaryButton is not null)
            {
                return await dialog.ShowDialog<bool>(App.Current.Services.GetRequiredService<ShellView>());;
            }
            
            await dialog.ShowDialog(App.Current.Services.GetRequiredService<ShellView>());
        }

        return false;
    }
}