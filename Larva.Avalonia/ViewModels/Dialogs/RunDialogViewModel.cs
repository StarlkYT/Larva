using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Larva.Avalonia.Models;
using Larva.Avalonia.Services;
using Larva.Avalonia.Views.Dialogs;
using Microsoft.Extensions.Logging;

namespace Larva.Avalonia.ViewModels.Dialogs;

public sealed partial class RunDialogViewModel : ObservableValidator
{
    public RunDialogView CurrentView { get; set; } = null!;

    [ObservableProperty]
    private Project project = null!;

    [ObservableProperty]
    [Required(ErrorMessage = "The Command Prefix field is required.")]
    private string commandPrefix = "!";

    [ObservableProperty]
    private bool commandCaseSensitive;

    [ObservableProperty]
    private bool isConnecting;

    [ObservableProperty]
    private bool hasConnected;

    [ObservableProperty]
    private string? username;

    [ObservableProperty]
    private string? discriminator;

    [ObservableProperty]
    private string? avatarUrl;

    [ObservableProperty]
    private string log = string.Empty;

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

        IsConnecting = true;

        var result = await discordClientService.ConnectAsync(project);

        if (result.IsFailed)
        {
            IsConnecting = false;
            await messageBoxDialogService.ShowAsync("Connection Failure", result.Errors[0].Message, CurrentView);
            return;
        }

        WriteLog("Connected successfully.");

        Username = result.Value.Username;
        Discriminator = $"#{result.Value.Discriminator}";
        AvatarUrl = result.Value.AvatarUrl;

        IsConnecting = false;
        HasConnected = true;

        CurrentView.Closed += async (_, _) => await discordClientService.DisconnectAsync();
    }

    [RelayCommand]
    private async Task DisconnectAsync()
    {
        WriteLog("Client has disconnected.", LogLevel.Critical);

        await discordClientService.DisconnectAsync();
        CurrentView.Close();
    }

    private void WriteLog(string message, LogLevel level = LogLevel.Information)
    {
        var builder = new StringBuilder(Log);

        builder.AppendLine($"{level.ToString()} @ {DateTime.UtcNow.ToString("HH:mm")}: {message}");

        Log = builder.ToString();
    }
}