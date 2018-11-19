using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Common;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthRepository(IOptions<JwtIssuerOptions> jwtOption)
        {
            _jwtOptions = jwtOption.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, IList<string> roles)
        {
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, userName),
               new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
               new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString()),
               identity.FindFirst(Consts.JwtClaimIdentifiers.Rol),
               identity.FindFirst(Consts.JwtClaimIdentifiers.Id)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claimsIdentity.Claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;

        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Consts.JwtClaimIdentifiers.Id, id),
                new Claim(Consts.JwtClaimIdentifiers.Rol, Consts.JwtClaims.ApiAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
