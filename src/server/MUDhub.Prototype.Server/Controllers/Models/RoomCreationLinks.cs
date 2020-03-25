using System.ComponentModel.DataAnnotations;

namespace MUDhub.Prototype.Server.Controllers.Models
{
    public class RoomCreationLinks
    {
        [Required]
        public int Room1 { get; set; }

        [Required]
        public int Room2 { get; set; }
    }
}