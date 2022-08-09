using System.Threading.Tasks;
using Avalonia.Controls;
using Larva.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Services;

public sealed class FileDialogService
{
    public async Task<string?> ShowAsync(string title = "Open File")
    {
        var dialog = new OpenFileDialog()
        {
            AllowMultiple = false,
            Title = title
        };

        var result = await dialog.ShowAsync(App.Current.Services.GetRequiredService<ShellView>());
        return result?.Length > 0 ? result[0] : null;
    }  
}