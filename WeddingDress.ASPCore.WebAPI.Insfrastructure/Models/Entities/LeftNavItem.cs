using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Common;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities
{
    public class LeftNavItem:EntityBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int LeftNavId { get; set; }
        //[ForeignKey("LeftNavId")] // here the correct place
        //public virtual LeftNav LeftNav { get; set; }
    }
}
