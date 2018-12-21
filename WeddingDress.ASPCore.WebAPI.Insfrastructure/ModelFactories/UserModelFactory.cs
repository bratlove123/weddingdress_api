using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces.ModelFactories;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.ModelFactories
{
    public class UserModelFactory : IUserModelFactory
    {
        public ApplicationUser CreateApplicationUser(UserViewModel model)
        {
            return Mapper.Map<UserViewModel, ApplicationUser>(model);
        }

        public UserViewModel CreateUser(ApplicationUser user)
        {
            return Mapper.Map<ApplicationUser, UserViewModel>(user);
        }
    }
}
