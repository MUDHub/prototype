using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using MUDhub.Prototype.Server.Hubs;
using MUDhub.Prototype.Server.Models;
using MUDhub.Prototype.Server.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Services
{
    public class NavigationService
    {
        private readonly RoomManager _roomManager;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly ILogger<NavigationService>? _logger;

        private readonly Dictionary<string, string> _activeRooms;

        private readonly string _roomToJoin;

        public NavigationService(RoomManager roomManager,
            IHubContext<GameHub> hubContext,
            ILogger<NavigationService>? logger = null)
        {
            _roomManager = roomManager;
            _hubContext = hubContext;
            _logger = logger;
            _activeRooms = new Dictionary<string, string>();
            _roomToJoin = roomManager.GetRooms().FirstOrDefault()?.Id ?? string.Empty;
        }


        // Maybe later target room id, for portals
        public NavigationResult NavigateToRoom(string userId, CardinalPoint direction)
        {
            //var user = await _userManager.GetUserByIdAsync(userId)
            //    .ConfigureAwait(false);

            //if (user is null)
            //{
            //    return new NavigationResult(false);
            //}


            //Later consistence checking
            var oldRoom = _roomManager.GetRoomById(_activeRooms[userId]);
            if (oldRoom is null)
            {
                return new NavigationResult(false,string.Empty);
            }

            switch (direction)
            {
                case CardinalPoint.North:
                    if (oldRoom.NorthId is null)
                    {
                        return new NavigationResult(false, string.Empty);
                    }
                    _activeRooms[userId] = oldRoom.NorthId; //Physical handling

                    break;
                case CardinalPoint.East:
                    if (oldRoom.EastId is null)
                    {
                        return new NavigationResult(false, string.Empty);
                    }
                    _activeRooms[userId] = oldRoom.EastId; //Physical handling
                    break;
                case CardinalPoint.West:
                    if (oldRoom.WestId is null)
                    {
                        return new NavigationResult(false, string.Empty);
                    }
                    _activeRooms[userId] = oldRoom.WestId; //Physical handling
                    break;
                case CardinalPoint.South:
                    if (oldRoom.SouthId is null)
                    {
                        return new NavigationResult(false, string.Empty);
                    }
                    _activeRooms[userId] = oldRoom.SouthId; //Physical handling
                    break;
                default:
                    break;
            }


            var newRoom = _roomManager.GetRoomById(_activeRooms[userId]);
            if (newRoom is null)
            {
                _activeRooms[userId] = oldRoom.Id;
                return new NavigationResult(false, string.Empty);
            }

            // Add event messages
            NotifyClient(userId, newRoom.EnterMessage);
            return new NavigationResult(true, newRoom.EnterMessage);
        }


        public NavigationResult UserJoinedTheWorld(string userId)
        {
            //Later consistence checking
            _activeRooms.Add(userId, _roomToJoin);
            var room = _roomManager.GetRoomById(_roomToJoin);
            //add Event messages
            NotifyClient(userId, room!.EnterMessage);
            return new NavigationResult(true, room!.EnterMessage);
        }

        private void NotifyClient(string userid, string message)
        {
            //Fire and forget, no awaiting.
            _hubContext.Clients.User(userid)
                .SendAsync(nameof(IGameClientContract.ReceiveGameMessage),message);
        }

    }
}
