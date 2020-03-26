using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MUDhub.Prototype.Server.Controllers;
using MUDhub.Prototype.Server.Controllers.Models;
using MUDhub.Prototype.Server.Models;
using MUDhub.Prototype.Server.Services.Models;
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
                    Name = "Kantine",
                    EnterMessage = "Sie betreten die Kantine.",
                    Description = "Eine Kantine, in der es mal mehr, mal weniger versalzenes Essen gibt",
                    X = 2,
                    Y = 0
                },
                new RoomCreations
                {
                    Id = 2,
                    Name = "Flur",
                    EnterMessage = "Du befindest dich in einem endlos scheinenden Flur...",
                    Description = "Ein Gang",
                    X = 1,
                    Y = 0
                },
                new RoomCreations
                {
                    Id = 3,
                    Name = "Toiletten",
                    EnterMessage = "Du betrittst das Klo, Händewaschen nicht vergessen!",
                    Description = "Das ist das Klo",
                    X = 0,
                    Y = 0
                },
                new RoomCreations
                {
                    Id = 4,
                    Name = "Flur",
                    EnterMessage = "",
                    Description = "",
                    X = 1,
                    Y = 1
                },
                new RoomCreations
                {
                    Id = 5,
                    Name = "Büro",
                    EnterMessage = "Ein leerstehendes Büro. Wem das wohl gehört...?",
                    Description = "Eine Türe zu einen kleinen Büro",
                    X = 2,
                    Y = 1
                },

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
                },
                new RoomCreationLinks
                {
                    Room1 = 2,
                    Room2 = 4
                },
                new RoomCreationLinks
                {
                    Room1 = 4,
                    Room2 = 5
                },
            };
            CreateRooms(rooms, roomLinks);
        }

        public IEnumerable<(CardinalPoint ,Room)> GetRoomNeigbours(string id)
        {
            var room = GetRoomById(id);
            if (room is null)
            {
                //Todo: add exception message.
                throw new ArgumentException();
            }
            
            if (!(room.EastId is null))
            {
                yield return (CardinalPoint.East ,GetRoomById(room.EastId)!);
            }
            if (!(room.WestId is null))
            {
                yield return (CardinalPoint.West, GetRoomById(room.WestId)!);
            }
            if (!(room.NorthId is null))
            {
                yield return (CardinalPoint.North, GetRoomById(room.NorthId)!);
            }
            if (!(room.SouthId is null))
            {
                yield return (CardinalPoint.South, GetRoomById(room.SouthId)!);
            }
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
                Name = r.Name ?? string.Empty,
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
                else if (yDistance < 0)
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
