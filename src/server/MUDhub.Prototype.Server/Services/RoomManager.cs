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

        private readonly List<Room> _rooms;

        public RoomManager()
        {
            _rooms = new List<Room>();
            CreateDefaultRooms();
        }

        private void CreateDefaultRooms()
        {
            List<RoomCreations> rooms = new List<RoomCreations>()
            {
                new RoomCreations
                {
                    Id = 1,
                    EnterMessage = "Sie betreten die Kantine.",
                    Description = "Das ist die Kantine",
                    X = 0,
                    Y = 0
                },
                new RoomCreations
                {
                    Id = 2,
                    EnterMessage = "Sieht der Gang nicht toll aus?",
                    Description = "Das ist der Gang",
                    X = 1,
                    Y = 0
                },
                new RoomCreations
                {
                    Id = 3,
                    EnterMessage = "Mehr als 3 mal schütteln ist Vergnügen",
                    Description = "Das ist das Klo",
                    X = 2,
                    Y = 0
                }
            };

            List<RoomCreationLinks> roomLinks = new List<RoomCreationLinks>()
            {
                new RoomCreationLinks
                {
                    Room1 = 1,
                    Room2 = 2
                },
                new RoomCreationLinks
                {
                    Room1 = 2,
                    Room2 = 3
                }
            };
            CreateRooms(rooms, roomLinks);
        }

        public Room? GetRoom(int x, int y)
        {
            return _rooms.FirstOrDefault(r => r.Position == new Point(x, y));
        }
        public Room? GetRoomById(string id)
        {
            return _rooms.FirstOrDefault(r => r.Id == id);
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
                EnterMessage = r.EnterMessage ?? string.Empty,
                Description = r.Description ?? string.Empty,
                Position = new Point(r.X, r.Y),
            }); ;

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
                else if (xDistance < 0)
                {
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
