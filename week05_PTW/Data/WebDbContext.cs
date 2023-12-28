using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
//
namespace week05_PTW.Data
{
   public class WebDbContext : IdentityDbContext<AppUser>
        // public class WebDbContext : IdentityDbContext<IdentityUser>
    {
        public WebDbContext(DbContextOptions<WebDbContext> options)
           : base(options)
        {
        }

		public WebDbContext()
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Categories> Categories { get; set; } = null!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    }
}
