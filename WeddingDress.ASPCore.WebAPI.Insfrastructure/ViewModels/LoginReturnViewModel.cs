using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels
{
    public class LoginReturnViewModel
    {
        public string Id { get; set; }
        public string Expires_in { get; set; }
        public string Auth_token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
    }
}
