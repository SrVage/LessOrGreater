using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains.Interfaces
{
    public interface IPlayerGrain : IGrainWithStringKey
    {
        Task ConnectToGame();
        Task SendNumber(int number);
        Task SetRoomId(string roomId);
    }
}
