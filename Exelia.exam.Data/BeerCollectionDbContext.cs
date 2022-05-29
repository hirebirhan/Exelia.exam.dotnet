
using Microsoft.EntityFrameworkCore;

namespace Exelia.exam.Data
{
    public class BeerCollectionDbContext : DbContext
    {

        public BeerCollectionDbContext(DbContextOptions<BeerCollectionDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>()
                .Property(b => b.Name)
                .IsRequired();
            
            modelBuilder.Entity<Beer>()
                .HasMany(b => b.Ratings);
        }
        public DbSet<Beer> Beers { get; set; } 
        public DbSet<Rating> Ratings { get; set; }

    }
}