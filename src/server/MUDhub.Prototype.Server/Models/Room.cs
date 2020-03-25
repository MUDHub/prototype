using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Models
{
    public class Room
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = string.Empty;
        public string EnterMessage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        public Point Position { get; set; } = new Point(0, 0);


        //Later maybe redesign to a list of navigations?
        public string? WestId { get; set; } // maybe as reference of room
        public string? NorthId { get; set; }
        public string? SouthId { get; set; }
        public string? EastId { get; set; }


    }

    public struct Point
    {

        public Point(int x, int y)
        {
            X = x;
            Y = y;
            Uid = Guid.NewGuid().ToString();
        }

        [Key]
        public string Uid { get; set; }
        public int X { get;  }
        public int Y { get;  }

        public override bool Equals(object obj)
        {
            return obj is Point point ? point.X == X && point.Y == Y : false;
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }
}
