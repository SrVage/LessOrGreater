using Grains.Interfaces;
using Microsoft.Extensions.Logging;

namespace Grains
{
    internal sealed class PlayerGrain : Grain, IPlayerGrain
    {
        private readonly ILogger _logger;
        private string _roomId;

        public PlayerGrain(ILogger logger) {
            _logger = logger;
        }
        public async Task ConnectToGame()
        {
            _logger.LogInformation("Player connect to game");
            var gameGrain = GrainFactory.GetGrain<IGameGrain>("game");
            await gameGrain.AddPlayerToQueue(this);
        }

        public Task SendNumber(int number)
        {
            throw new NotImplementedException();
        }

        public Task SetRoomId(string roomId)
        {
            _roomId = roomId;
            _logger.LogInformation("Player taked room id: " + _roomId);
            return Task.CompletedTask;
        }
    }
}
