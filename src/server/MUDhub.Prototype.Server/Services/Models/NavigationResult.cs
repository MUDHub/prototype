using System.Security.Principal;

namespace MUDhub.Prototype.Server.Services.Models
{
    public class NavigationResult
    {


        public NavigationResult(bool succeeded, string message, string? roomId = null, string? oldRoomId = null)
        {
            Succeeded = succeeded;
            Message = message;
            RoomId = roomId;
            OldRoomId = oldRoomId;
        }


        public string Message { get; }

        public bool Succeeded { get; }

        public string? RoomId { get; }
        public string? OldRoomId { get; }
    }
}