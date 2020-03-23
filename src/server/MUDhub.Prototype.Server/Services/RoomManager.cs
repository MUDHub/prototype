using MUDhub.Prototype.Server.Controllers;
using MUDhub.Prototype.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Services
{
    public class RoomManager
    {

        private Dictionary<Point, Room> _rooms;

        public RoomManager()
        {
            _rooms = new Dictionary<Point, Room>();
            CreateRooms();
        }

        private void CreateRooms()
        {
            var r = new Room
            {
                Name = "Kantine",
                Position = new Point(0, 0)

            };
            _rooms.Add(r.Position,r);
            //list.Add();
            //list.Add(new Room
            //{
            //    Name = "Flur",
            //    EastId = list[0].Id,
            //    Position = new Point(0, 0)
            //});
            //list[0].WestId = list[1].Id;
        }

        public Room? GetRoom(int x, int y)
        {
            if (_rooms.ContainsKey(new Point(x,y)))
            {
                return _rooms[new Point(x, y)];
            }
            else
            {
                return null;
            }
        }
    }
}
