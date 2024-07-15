namespace Grains.Interfaces
{
    public interface IRoomGrain : IGrainWithStringKey
    {
        Task AddPlayer(IPlayerGrain player);
        Task StartGame();
        Task GuessNumber(int number, IPlayerGrain player);  
    }
}
