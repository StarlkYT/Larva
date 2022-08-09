using System;
using System.Threading.Tasks;
using DSharpPlus;
using FluentResults;
using Larva.Avalonia.Models;
using Microsoft.Extensions.Logging;

namespace Larva.Avalonia.Services;

public sealed class DiscordClientService
{
    private DiscordClient? client;

    public async Task<Result<(string Username, string Discriminator, string AvatarUrl)>> ConnectAsync(Project project)
    {
        client = new DiscordClient(new DiscordConfiguration()
        {
            Token = project.Token
        });

        try
        {
            await client.ConnectAsync();

            return Result.Ok((client.CurrentUser.Username, client.CurrentUser.Discriminator,
                client.CurrentUser.AvatarUrl));
        }
        catch (Exception)
        {
            return Result.Fail("Invalid token.");
        }
    }

    public async Task DisconnectAsync()
    {
        if (client is null)
        {
            return;
        }

        await client.DisconnectAsync();
    }
}