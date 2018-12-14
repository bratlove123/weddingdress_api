using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUser(ApplicationUser user, string password);
        Task<List<ApplicationUser>> GetUsers();
    }
}
