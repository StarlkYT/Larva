using DSharpPlus;

namespace Larva.Client;

public sealed class Client : BackgroundService
{
    private readonly ILogger<Client> logger;
    private readonly DiscordClient client;

    public Client(ILogger<Client> logger, DiscordClient client)
    {
        this.logger = logger;
        this.client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await client.ConnectAsync();
        logger.LogInformation("The client has successfully connected");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await client.DisconnectAsync();
        logger.LogInformation("The client has disconnected");

        await base.StopAsync(cancellationToken);
    }
}