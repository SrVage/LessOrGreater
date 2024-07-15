using Common;
using Microsoft.Extensions.Hosting;

internal sealed class Program
{
    private static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
        .UseOrleans(siloBuilder =>
        {
            siloBuilder
            .UseLocalhostClustering()
            .AddMemoryGrainStorage(Constants.STORAGE_NAME);
        })
        .Build();

        await host.StartAsync();
        Console.ReadKey();
        await host.StopAsync();
    }
}