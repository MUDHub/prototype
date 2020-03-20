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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly RoomManager _roomManager;

        public RoomsController(RoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        [HttpGet()]
        public IEnumerable<Room> GetRooms()
        {
            return _roomManager.GetDummyRooms();
        }


    }
}