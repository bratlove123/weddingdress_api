using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
