using CatApiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CatApiApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CatEntity> Cats { get; set; }
        public DbSet<TagEntity> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many relationship between Cats and Tags
            modelBuilder.Entity<CatEntity>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Cats);
        }
    }
}
