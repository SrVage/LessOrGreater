namespace Grains.Interfaces
{
    public interface IPlayerGrain : IGrainWithStringKey
    {
        Task ConnectToGame();
        Task SendNumber(int number);
        void SetRoom(IRoomGrain roomGrain);
    }
}
