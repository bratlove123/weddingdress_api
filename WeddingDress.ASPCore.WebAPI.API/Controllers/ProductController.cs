using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models;
using WeddingDress.ASPCore.WebAPI.Services.Services;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WeddingDress.ASPCore.WebAPI.API.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("GetProducts")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}