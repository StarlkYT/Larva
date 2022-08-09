using DiscordRPC;

namespace Larva.Avalonia.Services;

public sealed class DiscordRichPresenceService
{
    private readonly DiscordRpcClient richPresenceClient;

    public DiscordRichPresenceService()
    {
        richPresenceClient = new DiscordRpcClient("1005218474169217035");
        richPresenceClient.Initialize();
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