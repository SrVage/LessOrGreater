using Grains.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    internal class RoomGrain : Grain, IRoomGrain
    {
        private readonly ILogger _logger;
        private IPlayerGrain player1;
        private IPlayerGrain player2;

        public RoomGrain(ILogger logger)
        {
            _logger = logger;
        }

        public Task AddPlayer(IPlayerGrain player)
        {
            if (player1 == null)
            {
                player1 = player;
                _logger.LogInformation("Player 1 was added");
            }
            else
            {
                player2 = player;
                _logger.LogInformation("Player 2 was added");
            }
            return Task.CompletedTask;
        }

        public Task StartGame()
        {
            throw new NotImplementedException();
        }
    }
}
