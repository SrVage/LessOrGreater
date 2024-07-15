namespace Grains.Interfaces
{
    public interface IPlayerObserver : IGrainObserver
    {
        Task EnterInRoom();
        Task GetResult(bool win, int guessNumber);
    }
}
