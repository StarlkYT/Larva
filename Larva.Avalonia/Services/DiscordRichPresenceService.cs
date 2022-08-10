using CommunityToolkit.Mvvm.Messaging;
using DiscordRPC;
using Larva.Avalonia.Messages;

namespace Larva.Avalonia.Services;

public sealed class DiscordRichPresenceService
{
    private readonly DiscordRpcClient richPresenceClient;

    public DiscordRichPresenceService()
    {
        richPresenceClient = new DiscordRpcClient("1005218474169217035");
        richPresenceClient.Initialize();

        WeakReferenceMessenger.Default.Register<ShellCloseMessage>(this, (_, _) => richPresenceClient.Deinitialize());
    }

    public void Update(string name, string description)
    {
        richPresenceClient.SetPresence(new RichPresence()
        {
            State = description,
            Details = name,
            Assets = new Assets()
            {
                LargeImageKey = "larva"
            }
        });
    }
}