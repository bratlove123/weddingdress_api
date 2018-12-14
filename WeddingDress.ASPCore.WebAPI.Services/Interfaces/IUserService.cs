using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(UserViewModel model);

        Task<List<ApplicationUser>> GetUsers();
    }
}
