using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Common
{
    public static class Consts
    {
        public struct JwtClaimIdentifiers
        {
            public const string Rol = "rol", Id = "id";
        }

        public struct JwtClaims
        {
            public const string ApiAccess = "api_access";
        }
    }
}
