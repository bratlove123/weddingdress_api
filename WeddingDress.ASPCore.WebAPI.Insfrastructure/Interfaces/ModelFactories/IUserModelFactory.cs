using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces.ModelFactories
{
    public interface IUserModelFactory
    {
        ApplicationUser CreateApplicationUser(UserViewModel model);
        UserViewModel CreateUser(ApplicationUser user);
    }
}
