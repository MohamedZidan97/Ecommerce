
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



            builder.Entity<Customer>().HasMany<Order>().WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>().HasMany<OrderItem>().WithOne()
        .OnDelete(DeleteBehavior.Cascade);


            //Cart
            builder.Entity<Customer>().HasOne<Cart>().WithOne()
.HasForeignKey<Cart>(e => e.CustomerId)
.HasPrincipalKey<Customer>(e => e.CustomerId).OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Cart>().HasMany<CartItem>().WithOne()
        .OnDelete(DeleteBehavior.Cascade);


            //Product


            builder.Entity<Product>().HasMany<CartItem>().WithOne()
      .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>().HasMany<OrderItem>().WithOne()
      .OnDelete(DeleteBehavior.Cascade);

            // Category

            builder.Entity<Category>().HasMany<SubCategory>().WithOne()
     .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SubCategory>().HasMany<Product>().WithOne()
      .OnDelete(DeleteBehavior.Cascade);

            // 
            builder.Entity<Order>().HasOne<Payment>().WithOne()
           .HasForeignKey<Payment>(e => e.OrderId)
            .HasPrincipalKey<Order>(e => e.OrderId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Payment>().HasOne<Invoice>().WithOne()
          .HasForeignKey<Invoice>(e => e.PaymentId)
           .HasPrincipalKey<Payment>(e => e.PaymentId).OnDelete(DeleteBehavior.Cascade);


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
        public DbSet<Customer> customers { get; set; }
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
