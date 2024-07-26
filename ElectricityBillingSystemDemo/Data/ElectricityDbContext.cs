using ElectricityBillingSystemDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ElectricityBillingSystemDemo.Data
{
    public class ElectricityDbContext : DbContext
    {
        public ElectricityDbContext(DbContextOptions<ElectricityDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                 .Property(e => e.CustomerId)
                 .ValueGeneratedOnAdd();

            modelBuilder.Entity<Bill>()
                .HasOne<Customer>()           
                .WithMany()                    //each Customer can have many Bills
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Payment>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Payment>()
                .HasOne<Bill>()
                .WithMany()
                .HasForeignKey(p => p.BillId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }


}
