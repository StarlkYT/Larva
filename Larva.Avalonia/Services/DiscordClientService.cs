using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Exceptions;
using FluentResults;

namespace Larva.Avalonia.Services;

public sealed class DiscordClientService
{
    private DiscordClient? client;
    
    public async Task<Result<(string, string, string)>> ConnectAsync(string token)
    {
        client = new DiscordClient(new DiscordConfiguration()
        {
            Token = token
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
}