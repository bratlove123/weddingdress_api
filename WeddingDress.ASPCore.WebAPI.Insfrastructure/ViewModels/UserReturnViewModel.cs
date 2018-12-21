using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels
{
    public class UserReturnViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public int CountAll { get; set; }
    }
}
