using Grains.Interfaces;

namespace Grains
{
    internal sealed class GameGrain : Grain, IGameGrain
    {
        private readonly Queue<IPlayerGrain> _players = new Queue<IPlayerGrain>();
        public async Task AddPlayerToQueue(IPlayerGrain player)
        {
            _players.Enqueue(player);
            Console.WriteLine("Connect player to queue. Players in queue: " + _players.Count);
            if (_players.Count > 1)
            {
                var player1 = _players.Dequeue();
                var player2 = _players.Dequeue();
                IRoomGrain room = CreateRoom(out string roomId);

                await room.AddPlayer(player1, player2);

                await room.StartGame();
            }
        }

        private IRoomGrain CreateRoom(out string roomId)
        {
            roomId = Guid.NewGuid().ToString();
            var room = GrainFactory.GetGrain<IRoomGrain>(roomId);
            return room;
        }
    }
}
