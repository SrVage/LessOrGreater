using Grains.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(clientBuilder =>
        clientBuilder.UseLocalhostClustering())
    .Build();

        await host.StartAsync();

        var client = host.Services.GetRequiredService<IClusterClient>();
        var playerId = Guid.NewGuid().ToString();
        var player = client.GetGrain<IPlayerGrain>(playerId);
        await player.ConnectToGame();
        Console.WriteLine(player.GetPrimaryKeyString());
        Console.ReadKey();
        await host.StopAsync();
    }
}