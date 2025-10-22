
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Domain.Entities;

namespace Zain.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }


        protected override void OnModelCreating(ModelBuilder builder)
        {


            // User

            builder.Entity<ApplicationUser>()
                .HasMany(o => o.Orders).WithOne(a=>a.User)
                .HasForeignKey(o=>o.UserId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            // Order

            builder.Entity<Order>()
                .HasMany(o=>o.OrderItems).WithOne(oi=>oi.Order)
                .HasForeignKey(o => o.OrderId)
        .OnDelete(DeleteBehavior.Cascade);


            //Cart
         

            builder.Entity<Cart>().HasMany(c=>c.CartItems).WithOne(ci=>ci.Cart)
                    .HasForeignKey(ci => ci.CartId)
                    .OnDelete(DeleteBehavior.Cascade);


            //Product


            builder.Entity<Product>()
                .HasMany(p=>p.CartItems)
                .WithOne(ci=>ci.Product)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Product>()
                .HasMany(p=>p.OrderItems)
                .WithOne(oi=>oi.Product)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Category

            builder.Entity<Category>()
                .HasMany(c=>c.SubCategories)
                .WithOne(s=>s.Category)
                .HasForeignKey(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<SubCategory>()
                .HasMany(s=>s.Products)
                .WithOne(p=>p.SubCategory)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // 
            builder.Entity<Order>().HasOne(o=>o.Payment).WithOne(p=>p.Order)
           .HasForeignKey<Payment>(e => e.OrderId)
           .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Payment>().HasOne(p=>p.Invoice).WithOne(i=>i.Payment)
          .HasForeignKey<Invoice>(e => e.PaymentId)
           .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(builder);
        }






        // DbSets

        // category
        public DbSet<Category> categories { get; set; }
        public DbSet<SubCategory> subCategories { get; set; }

        // cart 
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> cartItems { get; set; }

         //
        public DbSet<Invoice> invoices { get; set; }
        //Order
        public DbSet<Order>  orders  { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        // Payment
        public DbSet<Payment>  payments { get; set; }

        // product 
        public DbSet<Product>  products { get; set; }

        // 





    }
}
