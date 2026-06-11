using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace FurnitureFactory.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MaterialCategory> MaterialCategories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyItem> SupplyItems { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Supply>()
                .HasRequired(s => s.Supplier)
                .WithMany(sup => sup.Supplies)
                .HasForeignKey(s => s.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supply>()
                .HasOne(s => s.Contract)
                .WithMany(c => c.Supplies)
                .HasForeignKey(s => s.ContractId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SupplyItem>()
                .HasOne(si => si.Supply)
                .WithMany(s => s.SupplyItems)
                .HasForeignKey(si => si.SupplyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupplyItem>()
                .HasOne(si => si.Material)
                .WithMany(m => m.SupplyItems)
                .HasForeignKey(si => si.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            // Обчислювані властивості — не зберігаються в БД
            modelBuilder.Entity<SupplyItem>()
                .Ignore(si => si.TotalPrice);

            modelBuilder.Entity<Employee>()
                .Ignore(e => e.FullName);
        }
    }
}