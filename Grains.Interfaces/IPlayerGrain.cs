using System.Diagnostics.Eventing.Reader;

namespace Grains.Interfaces
{
    public interface IPlayerGrain : IGrainWithStringKey
    {
        Task<bool> InRoom();
        Task ConnectToGame();
        Task SendNumber(int number);
        void ReceiveResults(int guessNumber, bool win);
        void SetRoom(IRoomGrain roomGrain);
        public Task Subscribe(IPlayerObserver observer);
        public Task UnSubscribe(IPlayerObserver observer);
    }
}
