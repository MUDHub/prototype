using MUDhub.Prototype.Server.Controllers;
using MUDhub.Prototype.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Services
{
    public class RoomManager
    {

        public IEnumerable<Room> GetDummyRooms()
        {
            var list = new List<Room>();
            list.Add(new Room
            {
                Name = "Kantine",
                Position = new Point(1, 0)

            });
            list.Add(new Room
            {
                Name = "Flur",
                EastId = list[0].Id,
                Position = new Point(0,0)
            });
            list[0].WestId = list[1].Id;

            return list;
        }
    }
}
