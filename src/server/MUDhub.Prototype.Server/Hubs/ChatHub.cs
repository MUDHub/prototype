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
        public Task SendMessage(string username, string message)
        {
            return Clients.All.SendAsync("receiveMessage", username, message, DateTime.Now);
        }


        /*
        public Task SendToRoom(int roomname, string message)
        {
            return Clients.Group("room"+roomname).SendAsync("receiveRoom", message);
        }

        public Task ChangeRoom(string room)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, room);
        }
        */


        public override Task OnConnectedAsync()
        {
            // Groups.AddToGroupAsync(Context.ConnectionId, "room1");
            return base.OnConnectedAsync();
        }
    }
}
