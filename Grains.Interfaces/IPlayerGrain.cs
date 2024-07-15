using System.Diagnostics.Eventing.Reader;

namespace Grains.Interfaces
{
    public interface IPlayerGrain : IGrainWithStringKey
    {
        Task<bool> InRoom();
        Task ConnectToGame();
        Task SendNumber(int number);
        void SetRoom(IRoomGrain roomGrain);
    }
}
