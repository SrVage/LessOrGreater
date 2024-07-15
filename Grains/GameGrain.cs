using Grains.Interfaces;

namespace Grains
{
    internal class GameGrain : Grain, IGameGrain
    {
        private readonly Queue<IPlayerGrain> _playerGrains = new Queue<IPlayerGrain>();
        public async Task AddPlayerToQueue(IPlayerGrain player)
        {
            _playerGrains.Enqueue(player);
            if (_playerGrains.Count > 1)
            {
                var player1 = _playerGrains.Dequeue();
                var player2 = _playerGrains.Dequeue();
                IRoomGrain room = CreateRoom(out string roomId);

                await player1.SetRoomId(roomId);
                await player2.SetRoomId(roomId);

                await room.AddPlayer(player1);
                await room.AddPlayer(player2);

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
