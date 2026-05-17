using Microsoft.EntityFrameworkCore;
using AssetService.Models;

namespace AssetService.Data
{
    public class AssetDbContext : DbContext
    {
        public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.AssetID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
                entity.Property(e => e.InstallationDate).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            });
        }
    }
}