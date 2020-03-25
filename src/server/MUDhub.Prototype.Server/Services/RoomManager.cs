using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MUDhub.Prototype.Server.Controllers;
using MUDhub.Prototype.Server.Controllers.Models;
using MUDhub.Prototype.Server.Models;
using SQLitePCL;
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

        private List<Room> _rooms;

        public RoomManager()
        {
            _rooms = new List<Room>();
            CreateDefaultRooms();
        }

        private void CreateDefaultRooms()
        {
            //var r = new Room
            //{
            //    Name = "Kantine",
            //    Position = (0, 0)
            //};
            //_rooms.Add(r.Position, r);
            //r = new Room
            //{
            //    Name = "Klo",
            //    Position = (1, 0)
            //};
            //_rooms.Add(r.Position, r);
            //r = new Room
            //{
            //    Name = "Flur",
            //    Position = (0, 1)
            //};
            //_rooms.Add(r.Position, r);
            //r = new Room
            //{
            //    Name = "Flur",
            //    Position = (1, 1)
            //};
            //_rooms.Add(r.Position, r);
            //r = new Room
            //{
            //    Name = "Flur",
            //    Position = (1, 2)
            //};
            //_rooms.Add(r.Position, r);
            //r = new Room
            //{
            //    Name = "Klassenzimmer 1a",
            //    Position = (1, 3)
            //};
            //_rooms.Add(r.Position, r);

            //r = new Room
            //{
            //    Name = "Klassenzimmer 1b",
            //    Position = (2, 2)
            //};
            //_rooms.Add(r.Position, r);
        }

        public Room? GetRoom(int x, int y)
        {
            return _rooms.FirstOrDefault(r => r.Position == new Point(x, y));
        }

        public NavigationResult NavigateToRoom(User user, InteractionCommand command, string? targetRoom = null)
        {
            throw new NotImplementedException();
            if ((command &
                   (InteractionCommand.GoEast |
                    InteractionCommand.GoNorth |
                    InteractionCommand.GoSouth |
                    InteractionCommand.GoWest)) == 0)
            {
                return new NavigationResult
                {
                    Succeeded = false,
                };
            }

        }

        public IEnumerable<Room> GetRooms() => _rooms;

        public void CreateRooms(IEnumerable<RoomCreations> rooms, IEnumerable<RoomCreationLinks> links)
        {
            if (rooms is null)
            {
                throw new ArgumentNullException(nameof(rooms));
            }
            if (links is null)
            {
                throw new ArgumentNullException(nameof(links));
            }

            var realRooms = rooms.ToDictionary(r => r.Id, r => new Room
            {
                Description = r.Description ?? string.Empty,
                Position = new Point(r.X, r.Y),
            });

            //resolve links
            foreach (var link in links)
            {
                var room1 = realRooms[link.Room1];
                var room2 = realRooms[link.Room2];

                // top left (0,0)
                // go left => x
                // go down => y
                var xDistance = room2.Position.X - room1.Position.X;
                var yDistance = room2.Position.Y - room1.Position.Y;

                if (xDistance > 0)
                {
                    room1.EastId = room2.Id;
                    room2.WestId = room1.Id;
                }
                else if (xDistance < 0) {
                    room1.WestId = room2.Id;
                    room2.EastId = room1.Id;
                }
                else if (yDistance > 0)
                {
                    room1.NorthId = room2.Id;
                    room2.SouthId = room1.Id;
                }
                else
                {
                    room1.SouthId = room2.Id;
                    room2.NorthId = room1.Id;
                }
            }

            _rooms.AddRange(realRooms.Values);
        }

        //internal void CreateRoom(string? name, (int X, int Y) p)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
