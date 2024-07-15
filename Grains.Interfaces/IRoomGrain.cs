namespace Grains.Interfaces
{
    public interface IRoomGrain : IGrainWithStringKey
    {
        Task StartGame();
        Task GuessNumber(int number, IPlayerGrain player);
        Task AddPlayer(IPlayerGrain player1, IPlayerGrain player2);
    }
}
