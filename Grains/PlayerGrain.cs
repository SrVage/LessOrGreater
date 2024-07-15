using Common;
using Grains.Interfaces;

namespace Grains
{
    internal sealed class PlayerGrain : Grain, IPlayerGrain
    {
        private IRoomGrain _roomGrain;
        private IPlayerObserver _playerObserver;
        private IPersistentState<PlayerState> _playerState;

        public PlayerGrain([PersistentState("playerState", Constants.STORAGE_NAME)] IPersistentState<PlayerState> state)
        {
            _playerState = state;
        }

        public async Task ConnectToGame()
        {
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
                _playerState.State.Score++;
                _playerState.WriteStateAsync();
            }
            _playerObserver.GetResult(win, guessNumber);
        }

        public async Task SendNumber(int number)
        {
            await _roomGrain.GuessNumber(number, this);
        }

        public void SetRoom(IRoomGrain roomGrain)
        {
            _roomGrain = roomGrain;
            _playerObserver.EnterInRoom();
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
