using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Controllers.Models
{
    public class CreateRoomsArgs
    {
        public RoomCreations[] Rooms { get; set; } = Array.Empty<RoomCreations>();

        public RoomCreationLinks[] Links { get; set; } = Array.Empty<RoomCreationLinks>();

    }
}
