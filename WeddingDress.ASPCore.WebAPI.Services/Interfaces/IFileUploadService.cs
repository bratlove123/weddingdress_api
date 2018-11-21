using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Services.Interfaces
{
    public interface IFileUploadService
    {
        FileUpload GetFile(int Id);
        int UploadFile(IFormFile file);
    }
}
