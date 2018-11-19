using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Ef
{
    public class WeddingDressDataContext : IdentityDbContext<ApplicationUser>
    {
        public WeddingDressDataContext(DbContextOptions<WeddingDressDataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<LeftNav> LeftNavs { get; set; }
        public DbSet<LeftNavItem> LeftNavItems { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
