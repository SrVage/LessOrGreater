using Grains.Interfaces;
using Microsoft.Extensions.Logging;

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

        public Task SendNumber(int number)
        {
            throw new NotImplementedException();
        }

        public void SetRoom(IRoomGrain roomGrain)
        {
            _roomGrain = roomGrain;
            Console.WriteLine("Player taked room id");
        }
    }
}
