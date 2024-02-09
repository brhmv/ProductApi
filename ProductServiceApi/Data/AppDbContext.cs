using Microsoft.EntityFrameworkCore;
using ProductServiceApi.Models.Entities;
using System.Globalization;

namespace ProductServiceApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public override int SaveChanges()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                if (entity.Entity is BaseEntity baseEntity)
                {
                    baseEntity.ModifiedTime = DateTime.Now;

                    if (entity.State == EntityState.Added)
                    {
                        baseEntity.CreatedTime = DateTime.Now;
                    }
                }
            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Products)
                .UsingEntity(j => j.ToTable("ProductTag"));
        }
    }
}