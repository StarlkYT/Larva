using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Message;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;

namespace Larva.Avalonia.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{

    [ObservableProperty]
    private Project? currentProject;

    public MenuViewModel MenuViewModel { get; }

    public ShellViewModel(MenuViewModel menuViewModel, DiscordRichPresenceService discordRichPresenceService)
    {
        MenuViewModel = menuViewModel;
        discordRichPresenceService.Update("Idling", string.Empty);

        WeakReferenceMessenger.Default.Register<ProjectCreateMessage>(this,
            (_, message) =>
            {
                CurrentProject = message.Project;
                MenuViewModel.CurrentProject = CurrentProject;
                discordRichPresenceService.Update($"Editing '{CurrentProject.Name}'", CurrentProject.Description);
            });
    }
}