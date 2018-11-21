using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Ef;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Repositories
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private WeddingDressDataContext _context;

        public FileUploadRepository(WeddingDressDataContext context)
        {
            _context = context;
        }

        public FileUpload GetFile(int Id)
        {
            return _context.FileUploads.Find(Id);
        }

        public int UploadFile(FileUpload file)
        {
            _context.FileUploads.Add(file);
            _context.SaveChanges();

            return file.Id;
        }
    }
}
