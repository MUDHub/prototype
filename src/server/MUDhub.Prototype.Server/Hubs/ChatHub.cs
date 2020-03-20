 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs
{
    public class ChatHub  : Hub
    {
        public Task SendMessage(string message)
        {
            return Clients.All.SendAsync("receiveMessage", message);
        }

        public Task SendToRoom(string roomname, string message)
        {
            return Clients.Group(roomname).SendAsync("receiveRoom", message);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "room1");
            return base.OnConnectedAsync();
        }
    }
}
