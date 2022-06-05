using Exelia.exam.Data;
using Microsoft.EntityFrameworkCore;
namespace Exelia.exam.unit.test
{
    public class FakeDbContext : BeerCollectionDbContext
    {
        public FakeDbContext() : base(new DbContextOptionsBuilder<FakeDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options)
        {
        }
    }
}