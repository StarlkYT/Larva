using DSharpPlus;
using Larva.Client;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient(_ => new DiscordClient(new DiscordConfiguration()
        {
            Token = context.Configuration.GetValue<string>("Secrets:Token")
        }));

        services.AddHostedService<Client>();
    })
    .Build();

await host.RunAsync();