using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Message;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;

namespace Larva.Avalonia.ViewModels;

public sealed partial class EditorViewModel : ObservableObject
{
    [ObservableProperty]
    private Project? project;

    [ObservableProperty]
    private Project[]? recentlyOpened;

    private readonly RecentProjectService recentProjectService;
    private readonly ProjectService projectService;
    private readonly MessageBoxDialogService messageBoxDialogService;

    public EditorViewModel(RecentProjectService recentProjectService, ProjectService projectService,
        MessageBoxDialogService messageBoxDialogService)
    {
        this.recentProjectService = recentProjectService;
        this.projectService = projectService;
        this.messageBoxDialogService = messageBoxDialogService;
        WeakReferenceMessenger.Default.Register<ProjectCreateMessage>(this, (_, message) => Project = message.Project);
    }

    public async Task FetchRecentAsync()
    {
        var recent = await recentProjectService.FetchAsync();

        if (recent is null || recent.Length == 0)
        {
            RecentlyOpened = null;
            return;
        }

        RecentlyOpened = recent;
    }

    [RelayCommand]
    private async Task OpenProjectAsync(Project recentProject)
    {
        var result = await projectService.OpenAsync(recentProject.Path);

        if (result.IsFailed)
        {
            RecentlyOpened = await recentProjectService.RemoveAsync(recentProject);
            await messageBoxDialogService.ShowAsync("Catastrophic Failure", result.Errors[0].Message);
        }
    }
}