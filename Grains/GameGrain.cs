using Grains.Interfaces;

namespace Grains
{
    internal class GameGrain : Grain, IGameGrain
    {
        private readonly Queue<IPlayerGrain> _playerGrains = new Queue<IPlayerGrain>();
        public async Task AddPlayerToQueue(IPlayerGrain player)
        {
            _playerGrains.Enqueue(player);
            Console.WriteLine("Connect player to queue. Players in queue: " + _playerGrains.Count);
            if (_playerGrains.Count > 1)
            {
                var player1 = _playerGrains.Dequeue();
                var player2 = _playerGrains.Dequeue();
                IRoomGrain room = CreateRoom(out string roomId);

                player1.SetRoom(room);
                player2.SetRoom(room);

                await Task.WhenAll(room.AddPlayer(player1), room.AddPlayer(player2));

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
