using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Task7FluentAPI.Models;

namespace Task7FluentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Unit> Units { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(a => new { a.OrderId, a.ItemId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(a => a.Item)
                .WithMany(b => b.OrderItem)
                .HasForeignKey(b => b.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(a => a.Order)
                .WithMany(b => b.OrderItem)
                .HasForeignKey(b => b.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasIndex(a => a.Price)
                    .IsUnique();

            modelBuilder.Entity<Unit>()
                .Property(a => a.UnitName)
                    .IsRequired();

            modelBuilder.Entity<Item>()
                .HasOne(a => a.Unit)
                .WithMany(b => b.Items)
                .HasForeignKey(b => b.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Unit>()
                .Property(a => a.UnitName)
                    .IsRequired();

            modelBuilder.Entity<Item>()
                .Property(a => a.Name)
                    .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
