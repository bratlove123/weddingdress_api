using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces
{
    public interface IFileUploadRepository
    {
        int UploadFile(FileUpload file);
        FileUpload GetFile(int Id);
    }
}
