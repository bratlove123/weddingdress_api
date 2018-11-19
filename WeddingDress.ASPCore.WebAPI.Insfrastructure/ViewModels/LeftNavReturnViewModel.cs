using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels
{
    public class LeftNavReturnViewModel
    {
        public IEnumerable<LeftNav> LeftNavs { get; set; }
        public int CountAll { get; set; }
    }
}
