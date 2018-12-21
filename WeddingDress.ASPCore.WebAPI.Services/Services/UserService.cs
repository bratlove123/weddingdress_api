using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces.ModelFactories;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;

namespace WeddingDress.ASPCore.WebAPI.Services.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IUserModelFactory _userModelFactory;

        public UserService(IUserRepository userRepository, IUserModelFactory userModelFactory)
        {
            _userRepository = userRepository;
            _userModelFactory = userModelFactory;
        }
        public async Task<bool> AddUser(UserViewModel model)
        {
            var appUser = _userModelFactory.CreateApplicationUser(model);
            return await _userRepository.AddUser(appUser, model.Password, model.Role);
        }

        public Task<List<ApplicationUser>> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public UserReturnViewModel GetUsersWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search)
        {
            UserReturnViewModel returnModel = new UserReturnViewModel()
            {
                Users = _userRepository.GetUsersWithPagingAndSorting(pageSize, pageNumber, orderBy, sort, search),
                CountAll = _userRepository.CountWithSearch(search)
            };
            return returnModel;
        }

        public Task<IdentityResult> UpdateUser(UserViewModel model)
        {
            return _userRepository.UpdateUser(model);
        }

        public Task<IdentityResult> DeleteUser(string id)
        {
            return _userRepository.DeleteUser(id);
        }

        public async Task<UserViewModel> GetUserById(string id)
        {
            var user = _userRepository.GetUserById(id);
            var userViewModel = _userModelFactory.CreateUser(user);
            var role = await _userRepository.GetRole(user);
            userViewModel.Role = role;
            return userViewModel;
        }

        public IEnumerable<string> GetAllRoles()
        {
            return _userRepository.GetAllRoles();
        }
    }
}
