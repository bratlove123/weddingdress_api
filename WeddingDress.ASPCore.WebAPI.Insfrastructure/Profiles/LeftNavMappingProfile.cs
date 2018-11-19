using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Profiles
{
    public class LeftNavMappingProfile:Profile
    {
        public LeftNavMappingProfile()
        {
            CreateMap<LeftNavViewModel, LeftNav>().ReverseMap();
        }
    }
}
