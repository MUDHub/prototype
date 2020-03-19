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
        public Task SendMessage(string user, string name)
        {
            return Clients.All.SendAsync("receiveMessage", user, name);
        }
    }
}
