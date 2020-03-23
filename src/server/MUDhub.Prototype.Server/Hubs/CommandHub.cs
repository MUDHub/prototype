using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MUDhub.Prototype.Server.Hubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs
{
    public class CommandHub : Hub<ICommandClientContract>
    {
        public Task SendMainMessage(SendMainMessageArgs args)
        {
            return Task.CompletedTask;
        }

        public Task<ActionRequestResult> SendActionRequest(ActionRequestArgs args)
        {
            return Task.FromResult(new ActionRequestResult
            {
                Succeeded = true
            });
        }
    }
}
