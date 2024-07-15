using Grains.Interfaces;

namespace Grains
{
    internal sealed class RoomGrain : Grain, IRoomGrain
    {
        private const int MAX_GUESS_VALUE = 100;
        private IPlayerGrain _player1;
        private IPlayerGrain _player2;
        private int _guessNumber;
        private int? _firstPlayerNumber;
        private int? _secondPlayerNumber;
        private readonly Random _random = new Random();

        public Task AddPlayer(IPlayerGrain player1, IPlayerGrain player2)
        {
            _player1 = player1;
            _player2 = player2;
            _player1.SetRoom(this);
            _player2.SetRoom(this);
            return Task.CompletedTask;
        }

        public Task StartGame()
        {
            _guessNumber = _random.Next(MAX_GUESS_VALUE);
            _firstPlayerNumber = null;
            _secondPlayerNumber = null;
            Console.WriteLine("Start new game");
            Console.WriteLine("Guessed number: " + _guessNumber);
            return Task.CompletedTask;
        }

        public Task GuessNumber(int number, IPlayerGrain player)
        {
            if (player.GetGrainId() == _player1.GetGrainId() && !_firstPlayerNumber.HasValue) 
            {
                _firstPlayerNumber = number;
                Console.WriteLine("First player number is: " + _firstPlayerNumber);
            }
            else if (player.GetGrainId() == _player2.GetGrainId() && !_secondPlayerNumber.HasValue)
            {
                _secondPlayerNumber = number;
                Console.WriteLine("Second player number is: " + _secondPlayerNumber);
            }
            if (_firstPlayerNumber.HasValue && _secondPlayerNumber.HasValue) 
            {
                CheckResults();
            }  
            return Task.CompletedTask;
        }

        private async void CheckResults()
        {
            Console.WriteLine("Check results");
            var firstPlayerDifference = Math.Abs(_guessNumber - _firstPlayerNumber.Value);
            var secondPlayerDifference = Math.Abs(_guessNumber - _secondPlayerNumber.Value);
            if (firstPlayerDifference < secondPlayerDifference) 
            {
                Console.WriteLine("First player win");
            }
            else if (secondPlayerDifference < firstPlayerDifference)
            {
                Console.WriteLine("Second player win");
            }
            else
            {
                Console.WriteLine("Nobody win");
            }
            _player1.ReceiveResults(_guessNumber, firstPlayerDifference < secondPlayerDifference);
            _player2.ReceiveResults(_guessNumber, secondPlayerDifference < firstPlayerDifference);
            await Task.Delay(2000);
            await StartGame();
        }
    }
}
