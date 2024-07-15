using Grains.Interfaces;

namespace Grains
{
    internal sealed class PlayerGrain : Grain, IPlayerGrain
    {
        private IRoomGrain _roomGrain;

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

        public async Task SendNumber(int number)
        {
            Console.WriteLine("Send number: " + number);
            await _roomGrain.GuessNumber(number, this);
        }

        public void SetRoom(IRoomGrain roomGrain)
        {
            _roomGrain = roomGrain;
            Console.WriteLine("Player taked room id");
        }
    }
}
