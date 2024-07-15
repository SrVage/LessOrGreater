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
        private IPlayerGrain player1;
        private IPlayerGrain player2;


        public Task AddPlayer(IPlayerGrain player)
        {
            if (player1 == null)
            {
                player1 = player;
                Console.WriteLine("Player 1 was added");
            }
            else
            {
                player2 = player;
                Console.WriteLine("Player 2 was added");
            }
            return Task.CompletedTask;
        }

        public Task StartGame()
        {
            Console.WriteLine("Start new game");
            return Task.CompletedTask;
        }
    }
}
