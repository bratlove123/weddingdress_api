using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models;

namespace WeddingDress.ASPCore.WebAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwt(ClaimsIdentity identity, string userName, JsonSerializerSettings serializerSettings, IList<string> roles);
        ClaimsIdentity GenerateClaimsIdentity(string username, string id);
    }
}
