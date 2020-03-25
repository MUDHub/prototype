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
        private readonly UserManager _userManager;
        private readonly NavigationService _navigationService;

        public UsersController(UserManager userService, NavigationService navigationService)
        {
            _userManager = userService;
            _navigationService = navigationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> LoginAsync([FromBody]AuthenticateModel model)
        {
            var result = await _userManager.LoginAsync(model.Username, model.Password)
                .ConfigureAwait(false);

            if (!result.Succeeded)
                return BadRequest(new { message = "Username or password is incorrect" });

            _navigationService.UserJoinedTheWorld(result.User!.Id);
            return Ok(result);
        }   

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResult>> RegisterAsnyc([FromBody]AuthenticateModel model)
        {
            var result = await _userManager.RegisterAsync(model.Username, model.Password, false)
                .ConfigureAwait(false);
            if (!result.Succeeded)
                return BadRequest();

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var list = new List<User>();
            await foreach (var user in _userManager.GetUsersAsync())
            {
                list.Add(user);
            }

            return Ok(list);
        }
    }
}