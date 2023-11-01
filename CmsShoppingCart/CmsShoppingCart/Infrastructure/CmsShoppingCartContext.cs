using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CmsShoppingCart.Infrastructure
{
    public class CmsShoppingCartContext : IdentityDbContext<AppUser>
    {
        public CmsShoppingCartContext(DbContextOptions
            <CmsShoppingCartContext> options) :base(options)
        {
            
        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        /*public DbSet<OrderDetails> OrdersDetails { get; set; }*/
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        /*        public DbSet<savedOrder> savedOrders { get; set; }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Review>()
                    .HasOne(r => r.AppUser)
                    .WithMany() // or WithOne() depending on your model
                    .HasForeignKey(r => r.UserId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            { Name = "Admin", NormalizedName = "ADMIN", Id = "admin-role" });


            modelBuilder.Entity<AppUser>().HasData(
                    new AppUser
                    {
                        Id = "admin-user",
                        Email = "admin@mail.com",
                        NormalizedEmail = "ADMIN@MAIL.COM",
                        NormalizedUserName = "ADMIN@MAIL.COM",
                        UserName = "admin@mail.com",
                        PasswordHash =
                                    new PasswordHasher<AppUser>().HashPassword(
                                            new AppUser { Email = "admin@mail.com", UserName = "admin" },
                                            "admin")
                    }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                    new IdentityUserRole<string> { UserId = "admin-user", RoleId = "admin-role" }
            );
        }




    }


    
}
