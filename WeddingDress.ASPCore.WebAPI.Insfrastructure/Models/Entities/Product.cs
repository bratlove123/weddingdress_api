using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Common;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
    }
}
