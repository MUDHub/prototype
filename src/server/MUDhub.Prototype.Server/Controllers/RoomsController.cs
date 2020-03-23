using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MUDhub.Prototype.Server.Models;
using MUDhub.Prototype.Server.Services;

namespace MUDhub.Prototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly RoomManager _roomManager;

        public RoomsController(RoomManager roomManager, UserManager userManager)
        {
            _roomManager = roomManager;
        }

        [HttpGet()]
        public IActionResult GetRooms([FromQuery]int? x = null, [FromQuery]int? y = null)
        {
            return Ok(_roomManager.GetRoom(x ?? 0, y ?? 0));
        }


    }
}