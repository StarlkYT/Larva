using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Larva.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.Views;

public sealed partial class EditorView : UserControl
{
    private EditorViewModel? viewModel;

    public EditorView()
    {
        InitializeComponent();
    }

    protected override async void OnInitialized()
    {
        await viewModel!.FetchRecentAsync();
        base.OnInitialized();
    }

    private void InitializeComponent()
    {
        viewModel = App.Current.Services.GetRequiredService<EditorViewModel>();
        DataContext = viewModel;

        AvaloniaXamlLoader.Load(this);
    }
}