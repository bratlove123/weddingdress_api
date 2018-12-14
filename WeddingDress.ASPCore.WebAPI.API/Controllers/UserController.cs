using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;

namespace WeddingDress.ASPCore.WebAPI.API.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Authorize(Roles = "Admin")]
    [Route("api/user")]
    public class UserController:Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody]UserViewModel model)
        {
            var checkUser = await _userService.AddUser(model);
            if (checkUser)
            {
                return Ok();
            }

            ModelState.AddModelError("Message", "Got some error went creating an user");
            return BadRequest(ModelState);
        }

        [HttpGet("getall")]
        public Task<List<ApplicationUser>> GetUsers()
        {
            var users = _userService.GetUsers();
            return users;
        }
    }
}
