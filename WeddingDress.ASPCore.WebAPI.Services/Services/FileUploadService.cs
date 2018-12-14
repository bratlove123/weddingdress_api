using Microsoft.AspNetCore.Http;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;

namespace WeddingDress.ASPCore.WebAPI.Services.Services
{
    public class FileUploadService : IFileUploadService
    {
        private IFileUploadRepository _fileUploadRepository;
        private IUtils _utils;
        public FileUploadService(IFileUploadRepository fileUploadRepository, IUtils utils)
        {
            _fileUploadRepository = fileUploadRepository;
            _utils = utils;
        }

        public FileUpload GetFile(int Id)
        {
            return _fileUploadRepository.GetFile(Id);
        }

        public int UploadFile(IFormFile file)
        {
            FileUpload fileUpload = new FileUpload();
            fileUpload.FileData = _utils.GetByteArrayFromFile(file);
            fileUpload.FileName = file.FileName;
            return _fileUploadRepository.UploadFile(fileUpload);
        }
    }
}
