using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MUDhub.Prototype.Server.Hubs.Models;
using MUDhub.Prototype.Server.Services;
using MUDhub.Prototype.Server.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs
{
    public class GameHub : Hub<IGameClientContract>
    {
        private readonly NavigationService _navigationService;

        public GameHub(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public NavigationResult TryEnterRoom(CardinalPoint point)
        {
            var result = _navigationService.NavigateToRoom(Context.UserIdentifier, point);
            return result;
        }


        public override Task OnConnectedAsync()
        {
            _navigationService.UserJoinedTheWorld(Context.UserIdentifier);
            return Task.CompletedTask;
        }
        // ReceiveGameMessage
    }
}
