using Microsoft.Extensions.Hosting;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
        .UseOrleans(siloBuilder =>
        {
            siloBuilder.UseLocalhostClustering();
        })
        .Build();

        await host.StartAsync();
        Console.ReadKey();
        await host.StopAsync();
    }
}