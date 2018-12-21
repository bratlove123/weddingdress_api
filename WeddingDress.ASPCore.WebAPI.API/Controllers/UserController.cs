using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [HttpGet("getsort")]
        public UserReturnViewModel GetLeftNavsWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search)
        {
            var users = _userService.GetUsersWithPagingAndSorting(pageSize, pageNumber, orderBy, sort, search);
            return users;
        }

        [HttpGet("get/{id}")]
        public Task<UserViewModel> GetUserById(string id)
        {
            return _userService.GetUserById(id);
        }

        [HttpPut("update")]
        public Task<IdentityResult> UpdateUser([FromBody]UserViewModel model)
        {
            return _userService.UpdateUser(model);
        }

        [HttpDelete("delete/{id}")]
        public Task<IdentityResult> DeleteUser(string id)
        {
            return _userService.DeleteUser(id);
        }

        [HttpGet("roles")]
        public IEnumerable<string> GetAllRoles()
        {
            return _userService.GetAllRoles();
        }
    }
}
