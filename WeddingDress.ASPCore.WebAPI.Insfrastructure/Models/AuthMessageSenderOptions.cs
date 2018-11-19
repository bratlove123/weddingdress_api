using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Models
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
