﻿using Grains.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using System;

internal class Program
{
    private const string EXIT_STRING = "exit";
    private static IPlayerGrain _player;
    private static IHost _host;

    private static async Task Main(string[] args)
    {
        _host = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(clientBuilder =>
        clientBuilder.UseLocalhostClustering())
    .Build();

        await _host.StartAsync();

        var client = _host.Services.GetRequiredService<IClusterClient>();
        var playerId = Guid.NewGuid().ToString();
        _player = client.GetGrain<IPlayerGrain>(playerId);
        IPlayerObserver observer = new Client.Client(_player, _host);
        var obj = client.CreateObjectReference<IPlayerObserver>(observer);
        Console.WriteLine(obj);
        await _player.Subscribe(obj);
        await _player.ConnectToGame();
        Console.WriteLine("Success connect to server with player id: " + _player.GetPrimaryKeyString());
        while (true) { }

       /* while (!await _player.InRoom())
        {
            await Task.Delay(1000);
        }*/
        /*while (true)
        {
            Console.WriteLine("Enter numer or type exit");
            var input = Console.ReadLine();
            if (EXIT_STRING.Equals(input))
            {
                break;
            }
            if (int.TryParse(input, out var number))
            {
                await _player.SendNumber(number);
                *//*Result result = await _player.GetResults();
                while (result == null)
                {
                    await Task.Delay(1000);
                    result = await _player.GetResults();
                }
                Console.WriteLine((result.Win) ? "Win! " : "Lose... " + "Guess number was: " + result.GuessNumber);*//*
            }
        }*/
       // await _host.StopAsync();
    }
}