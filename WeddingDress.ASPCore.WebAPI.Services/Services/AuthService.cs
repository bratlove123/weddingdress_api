using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models;
using WeddingDress.ASPCore.WebAPI.Services.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using System.Linq;
using Microsoft.Extensions.Options;

namespace WeddingDress.ASPCore.WebAPI.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthService(IAuthRepository authRepository, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _authRepository = authRepository;
            _jwtOptions = jwtOptions.Value;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string username, string id)
        {
            return _authRepository.GenerateClaimsIdentity(username, id);
        }

        public async Task<string> GenerateJwt(ClaimsIdentity identity, string userName, JsonSerializerSettings serializerSettings, IList<string> roles)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await _authRepository.GenerateEncodedToken(userName, identity, roles),
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
