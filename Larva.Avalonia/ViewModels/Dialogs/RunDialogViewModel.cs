using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;
using Larva.Avalonia.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;

namespace Larva.Avalonia.ViewModels.Dialogs;

public sealed partial class RunDialogViewModel : ObservableValidator
{
    [ObservableProperty]
    private Project project = null!;

    [ObservableProperty]
    [Required(ErrorMessage = "The Command Prefix field is required.")]
    private string commandPrefix = "!";

    [ObservableProperty]
    private bool commandCaseSensitive;

    private readonly DiscordClientService discordClientService;
    private readonly MessageBoxDialogService messageBoxDialogService;

    public RunDialogViewModel(DiscordClientService discordClientService,
        MessageBoxDialogService messageBoxDialogService)
    {
        this.discordClientService = discordClientService;
        this.messageBoxDialogService = messageBoxDialogService;
    }

    [RelayCommand]
    private async Task ContinueAsync()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            return;
        }

        var result = await discordClientService.ConnectAsync(project.Token);

        if (result.IsFailed)
        {
            await messageBoxDialogService.ShowAsync("Connection Failure", result.Errors[0].Message,
                App.Current.Services.GetRequiredService<RunDialogView>());
            return;
        }
    }
}