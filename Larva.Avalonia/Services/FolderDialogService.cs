using System.Threading.Tasks;
using Avalonia.Controls;
using Larva.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Services;

public sealed class FolderDialogService
{
    public async Task<string?> ShowAsync(string title = "Open Folder")
    {
        var dialog = new OpenFolderDialog
        {
            Title = title,
        };

        return await dialog.ShowAsync(App.Current.Services.GetRequiredService<ShellView>());
    }
}