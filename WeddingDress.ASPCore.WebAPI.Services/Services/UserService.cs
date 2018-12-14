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
            return await _userRepository.AddUser(appUser, model.Password);
        }

        public Task<List<ApplicationUser>> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}
