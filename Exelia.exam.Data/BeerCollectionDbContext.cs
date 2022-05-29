
using Microsoft.EntityFrameworkCore;

namespace Exelia.exam.Data
{
    public class BeerCollectionDbContext : DbContext
    {

        public BeerCollectionDbContext(DbContextOptions<BeerCollectionDbContext> options)
            : base(options)
        {
        }




        public DbSet<Beer> Beers { get; set; }

    }
}