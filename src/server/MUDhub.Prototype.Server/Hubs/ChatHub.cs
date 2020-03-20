 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
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

        public Task SendToRoom(int roomname, string message)
        {
            return Clients.Group("room"+roomname).SendAsync("receiveRoom", message);
        }

        public Task ChangeRoom(int number)
        {
            for (int i = 1; i < 5; i++)
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, "room" + i);
            }
            return Groups.AddToGroupAsync(Context.ConnectionId, "room" + number);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "room1");
            return base.OnConnectedAsync();
        }
    }
}
