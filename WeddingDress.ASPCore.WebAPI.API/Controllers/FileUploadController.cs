using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Net;

namespace WeddingDress.ASPCore.WebAPI.API.Controllers
{
    [Route("api/file")]
    public class FileUploadController : Controller
    {
        private IFileUploadService _fileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [Authorize(Policy = "ApiUser")]
        [Authorize(Roles = "Admin")]
        [HttpPost("upload")]
        public IActionResult UploadFile(IFormFile file)
        {
            var id = _fileUploadService.UploadFile(file);
            return Ok(id);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetFile(int id)
        {
            var file = _fileUploadService.GetFile(id);
            if (file == null)
            {
                ModelState.AddModelError("Message", "Cannot find the file");
                return BadRequest(ModelState);
            }
            MemoryStream ms = new MemoryStream(file.FileData);
            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new StreamContent(ms);
            //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            //{
            //    FileName = file.FileName
            //};
            //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            return File(ms, "application/octet-stream");
        }
    }
}
