using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeddingDress.ASPCore.WebAPI.API.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Authorize(Roles = "Admin")]
    [Route("api/leftnav")]
    public class LeftNavController : Controller
    {
        private ILeftNavService _leftNavService;

        public LeftNavController(ILeftNavService leftNavService)
        {
            _leftNavService = leftNavService;
        }

        [HttpGet("getall")]
        public IEnumerable<LeftNav> Get()
        {
            return _leftNavService.GetLeftNavs();
        }

        [HttpGet("get/{id}")]
        public Task<LeftNav> Get(int id)
        {
            var leftNav = _leftNavService.GetLeftNav(id);
            return leftNav; 
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody]LeftNavViewModel leftNav)
        {
            _leftNavService.AddNewLeftNav(leftNav);
            return Ok();
        }

        [HttpPut("edit")]
        public IActionResult Put([FromBody]LeftNavViewModel leftNav)
        {
            _leftNavService.EditLeftNav(leftNav);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _leftNavService.DeleteLeftNav(id);
            return Ok();
        }

        [HttpGet("getsort")]
        public DataReturnViewModel GetLeftNavsWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search)
        {
            var leftNavs = _leftNavService.GetLeftNavsWithPagingAndSorting(pageSize, pageNumber, orderBy, sort, search);
            return leftNavs;
        }
    }
}
