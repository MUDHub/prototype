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
            //CreateDefaultRooms();
            CreateThorsWorld();
        }

        private void CreateThorsWorld()
        {
            List<RoomCreations> rooms = new List<RoomCreations>()
            {
                new RoomCreations
                {
                    Id = 1,
                    Name = "Ljossalfheim",
                    EnterMessage = "Willkommen in Ljossalfheim",
                    Description = "Ljossalfheim ist die Heimat der Lichtelfen, eine lichtdurchflutete Region und weil das Licht zur Erkenntnis führt, " +
                    "wird dieser Welt der nach Verständnis und Harmonie strebende Geist zugeordnet."+
                    "Elfen sind leuchtende Wesen, die sich gerne in lichten Hainen, " +
                    "an Quellen, in der Nähe von Blumen und Bäumen, auf Hügeln, Felsen und an Wasserfällen aufhalten.",
                    X = 0,
                    Y = 0
                },
                new RoomCreations
                {
                    Id = 2,
                    Name = "Asgard",
                    EnterMessage = "Willkommen in Asgard",
                    Description = "Asgard ist die oberste Welt. " +
                    "Hier wohnen die Asengötter - an die Spitze der Hierarchie. " +
                    "In Asgardh gibt es diverse Hallen, in denen die nordischen Götter wohnen. " +
                    "In Wallhalla, eine der berühmtesten Hallen, wohnen die in der Schlacht gefallenen Helden. " +
                    "Der oberste Punkt nennt sich Hlidskjalf . " +
                    "Das ist der Thron von Odin. " +
                    "Von seinem Thron aus kann Odin alle Welten überblicken.",
                    X = 1,
                    Y = 0
                },
                new RoomCreations
                {
                    Id = 3,
                    Name = "Vanaheim",
                    EnterMessage = "Willkommen in Vanaheim",
                    Description = "Vanaheim ist die Welt der Wanen. " +
                    "Es ist das Reich der Grundmuster des Organischen und der Verschmelzung, ein Ort der in fruchtbarer und statischer Balance befindlichen Kräfte. " +
                    "Wasser ist das Element dieser Welt. " +
                    "Die Wanen werden als die alten Erdgottheiten, des Friedens, der Harmonie und Reichtums betrachtet.",
                    X = 2,
                    Y = 0
                },
                new RoomCreations
                {
                    Id = 4,
                    Name = "Nidavellir",
                    EnterMessage = "Willkommen in Nidavellir",
                    Description = "Nidavellir ist eine der Neun Welten des Weltenbaums. " +
                    "Die Heimat der Zwerge ist vor allem für seine Waffenschmiede bekannt, die sich der Energie eines Sterns bedient. " +
                    "Hier wurden unter anderem Mjölnir, Gungnir, Hofund und Sturmbrecher erschaffen. ",
                    X = 0,
                    Y = 1
                },
                new RoomCreations
                {
                    Id = 5,
                    Name = "Midgard",
                    EnterMessage = "Willkommen in Midgard",
                    Description = "Midgard ist die eigentliche Heimat der Menschen. " +
                    "Es ist die Ebene der materiell manifestierten Dinge und Ereignisse. " +
                    "Umschlungen wird Midhgard von der Weltenschlange, " +
                    "die das grundlegende Gesetzt des Lebens symbolisiert. " +
                    "Sie verschlingt sich selbst. ",
                    X = 1,
                    Y = 1
                },
                new RoomCreations
                {
                    Id = 6,
                    Name = "Jötunheim",
                    EnterMessage = "Willkommen in Jötunheim",
                    Description = "Jötunheim ist das Reich, in dem die Riesen heimisch sind. " +
                    "Die Riesen stehen für personifiziert Naturgewalten sind. " +
                    "Sie mögen für den Menschen oft bedrohlich und gefährlich sein. " +
                    "Dennoch sind sie ein Bestandteil des natürlichen Zyklus und damit lebenswichtig. " +
                    "Aus einem Riesen erschufen die Asen einst die Welt.",
                    X = 2,
                    Y = 1
                },
                new RoomCreations
                {
                    Id = 7,
                    Name = "Muspelheim",
                    EnterMessage = "Willkommen in Muspelheim",
                    Description = "Muspelheim ist das Reich des reinen Feuers, der Funken, der Elektrizität. " +
                    "Hier finden wir die Expansion, die Kraft reiner Energie, die sich ständig ausdehnt. " +
                    "In der nordischen Kosmologie entstand die Welt aus der dynamischen Wechselwirkung zwischen Feuer und Eis über und innerhalb eines leeren Abgrundes, des magischen geladenen Nichts.",
                    X = 0,
                    Y = 2
                },
                new RoomCreations
                {
                    Id = 8,
                    Name = "Niffelheim",
                    EnterMessage = "Willkommen in Niffelheim",
                    Description = "Niffelheim ist das Reich des reinen Eises, " +
                    "des Nebels und der tiefsten Finsternis. " +
                    "Seine vorherrschenden Bedingungen sind starke Konzentration, Begrenzung, Kontraktion, Magnetismus und dadurch Strukturgebung. " +
                    "Aus dem eisigen Niflheimr fließt Isa  - das Welteneis." +
                    " Isa verbindet sich mit den Funken aus Muspelsheim und erschafft damit Ymir und Audumla. " +
                    "Später wird Niflheimr in das Totenreich von Hel verlegt.",
                    X = 1,
                    Y = 2
                },
                new RoomCreations
                {
                    Id = 9,
                    Name = "Svartalfheim",
                    EnterMessage = "Willkommen in Svartalfheim",
                    Description = "Svartalfheim ist der dunkle Gegenpart zum Lichtelfenheim. " +
                    "Hier leben die Schwarzelfen, die Zwerge. " +
                    "Es ist im Weltenbaum gesehen eine unterirdische dunkle Welt, in der die Gestalt geformt, “geschmiedet” wird. " +
                    "Die  Märchen und Sagen von Zwergen beschreiben die Zwerge als überaus geschickte Handwerker. " +
                    "Viele von ihnen betreiben Bergbau. Andere sind in der Schmiedekunst und Glasbläserei meisterhaft. " +
                    "Viele der berühmten magischen Gegenstände stammen aus dem dunklen Reich der Zwerge. " +
                    "- natürlich auch Thors Hammer. ",
                    X = 2,
                    Y = 2
                },
                new RoomCreations
                {
                    Id = 10,
                    Name = "Hel",
                    EnterMessage = "Willkommen in Hel",
                    Description = "Hel ist das Totenreich. " +
                    "Einst war dies einfach der Ort, wo alle, die nicht im Kampf gestorben sind, nach ihrem Tod hinkommen." +
                    " In der späteren Prosa Edda schildert Snorri  das Gehege der Hel als riesengroß, außerordentlich hoch und von Gittern umgeben. " +
                    "Ihr Saal heißt Elend, Hunger ihre Schüssel, Gier ihr Messer, Träg ihr Knecht, Langsam ihre Magd, Einsturz ihre Schwelle, ihr Bett Kümmernis und ihr Vorhang dräuendes Unheil. " +
                    "Hel selbst ist halb schwarz und halb menschenfarbig, grimmig und furchtbar vom Aussehen.",
                    X = 1,
                    Y = 3
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
                    Room2 = 3,
                },
                new RoomCreationLinks
                {
                    Room1 = 4,
                    Room2 = 5,
                },
                new RoomCreationLinks
                {
                    Room1 = 2,
                    Room2 = 5,
                },
                new RoomCreationLinks
                {
                    Room1 = 5,
                    Room2 = 6,
                },
                new RoomCreationLinks
                {
                    Room1 = 7,
                    Room2 = 8,
                },
                new RoomCreationLinks
                {
                    Room1 = 5,
                    Room2 = 8,
                },
                new RoomCreationLinks
                {
                    Room1 = 8,
                    Room2 = 9,
                },
                new RoomCreationLinks
                {
                    Room1 = 8,
                    Room2 = 10,
                },
            };
            CreateRooms(rooms, roomLinks);
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

        public IEnumerable<(CardinalPoint, Room)> GetRoomNeigbours(string id)
        {
            var room = GetRoomById(id);
            if (room is null)
            {
                //Todo: add exception message.
                throw new ArgumentException();
            }

            if (!(room.EastId is null))
            {
                yield return (CardinalPoint.East, GetRoomById(room.EastId)!);
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
