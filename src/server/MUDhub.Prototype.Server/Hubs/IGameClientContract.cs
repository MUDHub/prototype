using MUDhub.Prototype.Server.Hubs.Models;
using System.Globalization;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs
{
    public interface IGameClientContract
    {
        public Task ReceiveMainMessage(string message, ChannelScope scope = ChannelScope.Public);

        public Task ReceivePrivateMessage(string message,(string Name, string uid) user);
    }

    
}