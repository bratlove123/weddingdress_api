using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, IList<string> roles);
        ClaimsIdentity GenerateClaimsIdentity(string username, string id);
    }
}
