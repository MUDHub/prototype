using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}