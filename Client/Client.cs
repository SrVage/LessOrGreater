using Grains.Interfaces;
using Microsoft.Extensions.Hosting;
using Common;

namespace Client
{
    internal sealed class Client : IPlayerObserver
    {
        private IPlayerGrain _player;
        private IHost _host;
        public Client(IPlayerGrain player, IHost host)
        {
            _player = player;
            _host = host;
        }
        public async Task EnterInRoom()
        {
            while (true)
            {
                Console.WriteLine("Enter number or type exit");
                var input = Console.ReadLine();
                if (Constants.EXIT_STRING.Equals(input))
                {
                    await _host.StopAsync();
                    break;
                }
                if (int.TryParse(input, out var number))
                {
                    await _player.SendNumber(number);
                    break;
                }
            }
        }

        public Task GetResult(bool win, int guessNumber)
        {
            Console.WriteLine($"{(win ? "Win!" : "Lose...")} Guess number was: {guessNumber}");
            return EnterInRoom();
        }
    }
}
