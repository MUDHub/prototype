using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MUDhub.Prototype.Server.Models;
using MUDhub.Prototype.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClientContract>
    {
        private readonly UserManager _userManager;

        public ChatHub(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task SendGlobalMessage(string message)
        {
            User user = await GetActualUserAsync()
                .ConfigureAwait(false);
            Clients.All.ReceiveGlobalMessage(message, user.Username);
        }

        public async Task SendPrivateMessage(string message, string username)
        {
            var targetUser = await _userManager.GetUserAsync(username)
                .ConfigureAwait(false);
            if (targetUser is null)
            {
                //Todo: handle later not with a exception.
                throw new InvalidOperationException();
            }

            var user =  await GetActualUserAsync()
                   .ConfigureAwait(false);
            Clients.User(targetUser.Id).ReceivePrivateMessage(message, user.Username);
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserByIdAsync(Context.UserIdentifier)
                .ConfigureAwait(false);
            //Maybe later Save Messages.
            await Clients.Others
                .ReceiveGlobalMessage($"{user?.Username ?? "Unkown"} ist dem Chat beigetreten.", "Server")
                .ConfigureAwait(false);
            await base.OnConnectedAsync()
                .ConfigureAwait(false);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userManager.GetUserByIdAsync(Context.UserIdentifier)
                .ConfigureAwait(false);
            //Maybe later Save Messages.
            await Clients.Others
                .ReceiveGlobalMessage($"{user?.Username ?? "Unkown"} hat den Chat verlassen.", "Server")
                .ConfigureAwait(false);
            await base.OnDisconnectedAsync(exception);
        }

        private async Task<User> GetActualUserAsync()
        {
            var user = await _userManager.GetUserByIdAsync(Context.UserIdentifier)
                .ConfigureAwait(false);
            if (user is null)
            {
                //Todo: handle later not with a exception.
                throw new InvalidOperationException();
            }
            return user;
        }
    }
}
