using AutoMapper;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Profiles
{
    public class RegistrationAppUserMappingProfile:Profile
    {
        public RegistrationAppUserMappingProfile()
        {
            CreateMap<RegistrationViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
