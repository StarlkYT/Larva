using Larva.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia;

public static class ViewModelLocator
{
    public static EditorViewModel EditorViewModel => App.Current.Services.GetRequiredService<EditorViewModel>();
}