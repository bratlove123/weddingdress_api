using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Models
{
    public enum Role
    {
        Admin,
        Role1,
        Role2
    }
    public static class RoleExtensions
    {
        public static string GetRoleName(this Role role)
        {
            return role.ToString();
        }
    }
}
