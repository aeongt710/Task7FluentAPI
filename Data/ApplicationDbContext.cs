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
        public DbSet<ItemUnit> ItemUnits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>(x =>
                {
                    x.HasKey(a => new { a.OrderId, a.ItemId });

                    x.HasOne(a => a.Order)
                        .WithMany(b => b.OrderItem)
                        .HasForeignKey(b => b.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);

                    x.HasOne(a => a.Item)
                        .WithMany(b => b.OrderItem)
                        .HasForeignKey(b => b.ItemId)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity<ItemUnit>(x =>
                {
                    x.HasKey(a => new { a.UnitId, a.ItemId });

                    x.HasOne(a => a.Unit)
                        .WithMany(b => b.ItemUnit)
                        .HasForeignKey(b => b.UnitId)
                        .OnDelete(DeleteBehavior.Cascade);

                    x.HasOne(a => a.Item)
                        .WithMany(b => b.ItemUnit)
                        .HasForeignKey(b => b.ItemId)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );

            modelBuilder.Entity<Unit>()
                .HasIndex(a => a.UnitName)
                    .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(a => a.OrderName)
                    .IsUnique();

            modelBuilder.Entity<Item>(x =>
                {
                    x.Property(a => a.Price)
                        .IsRequired();

                    x.Property(a => a.Name)
                        .IsRequired();

                    //x.HasOne(a => a.Unit)
                    //    .WithMany(b=>b.Items)
                    //    .HasForeignKey(c => c.UnitId)
                    //    .OnDelete(DeleteBehavior.Cascade);
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}