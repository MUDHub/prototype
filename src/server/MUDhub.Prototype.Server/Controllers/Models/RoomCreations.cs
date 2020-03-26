using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace MUDhub.Prototype.Server.Controllers.Models
{
    public class RoomCreations
    {

        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? EnterMessage { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }

    }
}