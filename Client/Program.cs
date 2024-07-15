using Grains.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
        .UseOrleansClient(clientBuilder =>
            clientBuilder.UseLocalhostClustering())
        .Build();

        await host.StartAsync();

        var client = host.Services.GetRequiredService<IClusterClient>();
        var playerId = Guid.NewGuid().ToString();
        var player = client.GetGrain<IPlayerGrain>(playerId);

        await CreatePlayerObserver(host, client, player);

        await player.ConnectToGame();
        Console.WriteLine("Success connect to server with player id: " + player.GetPrimaryKeyString());

        while (true) { }
    }

    private static async Task CreatePlayerObserver(IHost host, IClusterClient client, IPlayerGrain player)
    {
        IPlayerObserver observer = new Client.Client(player, host);
        var obj = client.CreateObjectReference<IPlayerObserver>(observer);
        await player.Subscribe(obj);
    }
}