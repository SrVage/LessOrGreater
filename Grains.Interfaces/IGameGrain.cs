namespace Grains.Interfaces
{
    public interface IGameGrain : IGrainWithStringKey
    {
        Task AddPlayerToQueue(IPlayerGrain player);
    }
}
