using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        private IUtils _utils;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUtils utils)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _utils = utils;
        }

        public async Task<bool> AddUser(ApplicationUser user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return false;
            }

            var currentUser = await _userManager.FindByEmailAsync(user.Email);

            var roleresult = await _userManager.AddToRoleAsync(currentUser, role);

            return true;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public int CountWithSearch(string searchStr)
        {
            if (!string.IsNullOrEmpty(searchStr))
            {
                return _userManager.Users.Count(p => p.UserName.Contains(searchStr) || p.Email.Contains(searchStr));
            }
            return _userManager.Users.Count();
        }

        public ApplicationUser GetUserById(string id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<string> GetRole(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        public async Task<IdentityResult> UpdateUser(ViewModels.UserViewModel user)
        {
            var userGet = GetUserById(user.Id);
            userGet.FirstName = user.FirstName;
            userGet.LastName = user.LastName;
            var newPassword = _userManager.PasswordHasher.HashPassword(userGet, user.Password);
            userGet.PasswordHash = newPassword;
            var roleresult = await _userManager.AddToRoleAsync(userGet, user.Role);
            return await _userManager.UpdateAsync(userGet);
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = GetUserById(id);
            return await _userManager.DeleteAsync(user);
        }

        public IEnumerable<ApplicationUser> GetUsersWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "Id";
            }

            IEnumerable<ApplicationUser> users = null;
            IOrderedQueryable<ApplicationUser> queryOrder = null;
            if (sort)
            {
                queryOrder = _utils.OrderBy(_userManager.Users, orderBy);
            }
            else
            {
                queryOrder = _utils.OrderByDescending(_userManager.Users, orderBy);
            }

            if (string.IsNullOrEmpty(search))
            {
                users = queryOrder
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            else
            {
                users = queryOrder
                    .Where(p => p.UserName.Contains(search) || p.Email.Contains(search))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }


            return users;
        }

        public IEnumerable<string> GetAllRoles()
        {
            return _roleManager.Roles.Select(r=>r.Name).OrderBy(r=>r).ToArray();
        }
    }
}
