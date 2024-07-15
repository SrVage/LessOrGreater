namespace Grains.Interfaces
{
    public interface IRoomGrain : IGrainWithStringKey
    {
        Task AddPlayer(IPlayerGrain player);
        Task StartGame();
    }
}
