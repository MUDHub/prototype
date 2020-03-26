using MUDhub.Prototype.Server.Hubs.Models;
using System.Globalization;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs
{
    public interface IGameClientContract
    {
        Task ReceiveGameMessage(string message);
    }

    
}