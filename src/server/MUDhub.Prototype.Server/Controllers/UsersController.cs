using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MUDhub.Prototype.Server.ApiModels;
using MUDhub.Prototype.Server.Models;
using MUDhub.Prototype.Server.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager _userService;

        public UsersController(UserManager userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> LoginAsync([FromBody]AuthenticateModel model)
        {
            var result = await _userService.LoginAsync(model.Username, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(result);
        }   

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResult>> RegisterAsnyc([FromBody]AuthenticateModel model)
        {
            var result = await _userService.RegisterAsync(model.Username, model.Password, false);
            if (!result.Succeeded)
                return BadRequest();

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var list = new List<User>();
            await foreach (var user in _userService.GetUsersAsync())
            {
                list.Add(user);
            }

            return Ok(list);
        }
    }
}