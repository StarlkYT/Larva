using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Messages;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;
using Larva.Models;
using Larva.Models.Events;

namespace Larva.Avalonia.ViewModels;

public sealed partial class EditorViewModel : ObservableObject
{
    [ObservableProperty]
    private EventsViewModel eventsViewModel;

    [ObservableProperty]
    private Project? project;

    [ObservableProperty]
    private Recent[]? recentlyOpened;

    private readonly RecentProjectService recentProjectService;
    private readonly ProjectService projectService;
    private readonly MessageBoxDialogService messageBoxDialogService;

    public EditorViewModel(RecentProjectService recentProjectService, ProjectService projectService,
        MessageBoxDialogService messageBoxDialogService, EventsViewModel eventsViewModel)
    {
        this.recentProjectService = recentProjectService;
        this.projectService = projectService;
        this.messageBoxDialogService = messageBoxDialogService;
        this.eventsViewModel = eventsViewModel;

        WeakReferenceMessenger.Default.Register<ProjectCreateMessage>(this, (_, message) =>
        {
            Project = message.Project;
            
            if (Project.Root?.Events is not null)
            {
                EventsViewModel.Events = new ObservableCollection<EventBase>(Project.Root.Events);

                if (EventsViewModel.Events.Count > 0)
                {
                    EventsViewModel.CurrentEvent = EventsViewModel.Events[0];
                    EventsViewModel.HasEvents = true;    
                }
            }
        });

        WeakReferenceMessenger.Default.Register<ProjectSaveMessage>(this, async (_, _) =>
        {
            if (this.eventsViewModel.IsDirty)
            {
                if (await messageBoxDialogService.ShowAsync("Unsaved Changes", "Please save your project.", "Save") is
                    true)
                {
                    Project!.Root = new Root()
                    {
                        Events = eventsViewModel.Events.ToArray()
                    };
                    
                    var result = await projectService.SaveAsync(Project!);

                    if (result.IsFailed)
                    {
                        await messageBoxDialogService.ShowAsync("Catastrophic Failure", result.Errors[0].Message);
                        return;
                    }

                    WeakReferenceMessenger.Default.Send<ConfirmShellCloseMessage>();
                }
                else
                {
                    return;
                }
            }

            WeakReferenceMessenger.Default.Send<ConfirmShellCloseMessage>();
        });
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
    private async Task OpenProjectAsync(Recent recentProject)
    {
        var result = await projectService.OpenAsync(recentProject.Path);

        if (result.IsFailed)
        {
            RecentlyOpened = await recentProjectService.RemoveAsync(result.Value);
            await messageBoxDialogService.ShowAsync("Catastrophic Failure", result.Errors[0].Message);
        }
    }
}