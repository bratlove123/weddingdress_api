using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual long? FacebookId { get; set; }
        public virtual string PictureUrl { get; set; }
    }
}
