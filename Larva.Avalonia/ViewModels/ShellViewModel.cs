using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Message;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;
using Microsoft.Extensions.Logging;

namespace Larva.Avalonia.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{

    [ObservableProperty]
    private Project? currentProject;

    public MenuViewModel MenuViewModel { get; }

    public ShellViewModel(ILogger<ShellViewModel> logger, MenuViewModel menuViewModel, DiscordRichPresenceService discordRichPresenceService)
    {
        MenuViewModel = menuViewModel;
        discordRichPresenceService.Update("Idling.", string.Empty);
        logger.LogInformation("Updated rich presence to idle");

        WeakReferenceMessenger.Default.Register<ProjectCreateMessage>(this,
            (_, message) =>
            {
                CurrentProject = message.Project;
                MenuViewModel.CurrentProject = CurrentProject;
                discordRichPresenceService.Update($"Editing '{CurrentProject.Name}'.", CurrentProject.Description);
                
                logger.LogInformation("Opened a new project");
            });
    }
}