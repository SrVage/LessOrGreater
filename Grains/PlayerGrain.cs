using Grains.Interfaces;

namespace Grains
{
    internal sealed class PlayerGrain : Grain, IPlayerGrain
    {
        private IRoomGrain _roomGrain;
        private int _score = 0;
        private IPlayerObserver _playerObserver;

        public async Task ConnectToGame()
        {
            Console.WriteLine("Player connect to game");
            var gameGrain = GrainFactory.GetGrain<IGameGrain>("game");
            await gameGrain.AddPlayerToQueue(this);
        }

        public Task<bool> InRoom()
        {
            return Task.FromResult(_roomGrain != null);
        }

        public void ReceiveResults(int guessNumber, bool win)
        {
            if (win)
            {
                _score++;
            }
            Console.WriteLine("Player is " + (win ? "win" : "lose"));
            Console.WriteLine("Player's general score: " + _score);
            _playerObserver.GetResult(win, guessNumber);
        }

        public async Task SendNumber(int number)
        {
            Console.WriteLine("Send number: " + number);
            await _roomGrain.GuessNumber(number, this);
        }

        public void SetRoom(IRoomGrain roomGrain)
        {
            _roomGrain = roomGrain;
            _playerObserver.EnterInRoom();
            Console.WriteLine("Player taked room id");
        }

        public Task Subscribe(IPlayerObserver observer)
        {
            _playerObserver = observer;

            return Task.CompletedTask;
        }

        public Task UnSubscribe(IPlayerObserver observer)
        {
            if (_playerObserver == observer)
            {
                _playerObserver = null;
            }

            return Task.CompletedTask;
        }
    }
}
