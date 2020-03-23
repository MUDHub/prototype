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

        private Dictionary<(int X, int Y), Room> _rooms;

        public RoomManager()
        {
            _rooms = new Dictionary<(int X, int Y), Room>();
            CreateRooms();
        }

        private void CreateRooms()
        {
            var r = new Room
            {
                Name = "Kantine",
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
            if (_rooms.ContainsKey((x,y)))
            {
                return _rooms[(x, y)];
            }
            else
            {
                return null;
            }
        }

        internal void CreateRooms((string Name, int X, int Y)[] rooms)
        {
            throw new NotImplementedException();
        }

        internal void CreateRoom(string? name, (int X, int Y) p)
        {
            throw new NotImplementedException();
        }
    }
}
