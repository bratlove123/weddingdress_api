using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUser(ApplicationUser user, string password, string role);
        Task<List<ApplicationUser>> GetUsers();
        IEnumerable<ApplicationUser> GetUsersWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search);
        int CountWithSearch(string searchStr);
        ApplicationUser GetUserById(string id);
        Task<IdentityResult> UpdateUser(ViewModels.UserViewModel user);
        Task<IdentityResult> DeleteUser(string id);
        IEnumerable<string> GetAllRoles();
        Task<string> GetRole(ApplicationUser user);
    }
}
