using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Common;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities
{
    public class Project:EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public string[] ImageUrls { get; set; }
        //public string[] VideoUrls { get; set; }
    }
}
