using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Common;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities
{
    public class FileUpload: EntityBase
    {
        public string FileName
        {
            get;
            set;
        }
        public byte[] FileData
        {
            get;
            set;
        }
    }
}
