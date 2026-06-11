using System.Collections.Generic;
using System.Data.Entity;

namespace FurnitureFactory.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("FurnitureFactoryDB") { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MaterialCategory> MaterialCategories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyItem> SupplyItems { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Supply -> Supplier (без каскадного видалення)
            modelBuilder.Entity<Supply>()
                .HasRequired(s => s.Supplier)
                .WithMany(sup => sup.Supplies)
                .HasForeignKey(s => s.SupplierId)
                .WillCascadeOnDelete(false);

            // Supply -> Contract (необов'язковий зв'язок)
            modelBuilder.Entity<Supply>()
                .HasOptional(s => s.Contract)
                .WithMany(c => c.Supplies)
                .HasForeignKey(s => s.ContractId)
                .WillCascadeOnDelete(false);

            // SupplyItem -> Supply
            modelBuilder.Entity<SupplyItem>()
                .HasRequired(si => si.Supply)
                .WithMany(s => s.SupplyItems)
                .HasForeignKey(si => si.SupplyId)
                .WillCascadeOnDelete(true);

            // SupplyItem -> Material
            modelBuilder.Entity<SupplyItem>()
                .HasRequired(si => si.Material)
                .WithMany(m => m.SupplyItems)
                .HasForeignKey(si => si.MaterialId)
                .WillCascadeOnDelete(false);

            // TotalPrice — обчислювана властивість, не зберігається в БД
            modelBuilder.Entity<SupplyItem>()
                .Ignore(si => si.TotalPrice);

            // Employee FullName — не зберігається в БД
            modelBuilder.Entity<Employee>()
                .Ignore(e => e.FullName);
        }
    }
}