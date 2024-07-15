using Grains.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Client
{
    internal class Client : IPlayerObserver
    {
        private const string EXIT_STRING = "exit";
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
                Console.WriteLine("Enter numer or type exit");
                var input = Console.ReadLine();
                if (EXIT_STRING.Equals(input))
                {
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
            Console.WriteLine((win ? "Win! " : "Lose... ") + "Guess number was: " + guessNumber);
            return EnterInRoom();
        }
    }
}
