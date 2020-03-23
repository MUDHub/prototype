﻿using System;
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

        public (int X, int Y) Position { get; set; } = (0, 0);

        public string? WestId { get; set; } // maybe as reference of room
        public string? NorthId { get; set; }
        public string? SouthId { get; set; }
        public string? EastId { get; set; }

        
    }

    public class Point
    {

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        [Key]
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public int X { get; set; }
        public int Y { get; set; }

    }
}
