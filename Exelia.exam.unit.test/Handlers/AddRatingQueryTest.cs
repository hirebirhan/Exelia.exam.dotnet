using Exelia.exam.Business.CQRS.Commands.UpdateBeer;
using Exelia.exam.Business.CQRS.Queries;
using Xunit;

namespace Exelia.exam.unit.test.Handlers
{
    public class AddRatingQueryTest
    {
        private readonly FakeDbContext fakeDbContext;
        private readonly AddBeerRatingQuery ratingQuery;

        public AddRatingQueryTest()
        {
            fakeDbContext = new FakeDbContext();
            ratingQuery = new AddBeerRatingQuery(fakeDbContext);
        }

        [Fact]
        public async Task ShouldAddRatingIfCommandIsValid()
        {
            fakeDbContext.Beers.Add(new Data.Beer() { Name = "Test beer", CreatedDate = DateTimeOffset.Now });
            CancellationToken cancellationToken = new();
            await fakeDbContext.SaveChangesAsync(cancellationToken);
            AddBeerRatingCommand command = new (1, 1);
            var result = await ratingQuery.Handle(command, cancellationToken);
            Assert.NotNull(result);
            Assert.True(result.Success);

        }


        [Fact]
        public async Task ShouldNoAddRatingIfBeerIdIsNotValid()
        {
            fakeDbContext.Beers.Add(new Data.Beer() { Name = "Test beer", CreatedDate = DateTimeOffset.Now });
            CancellationToken cancellationToken = new();
            await fakeDbContext.SaveChangesAsync(cancellationToken);
            AddBeerRatingCommand command = new(1, 5);// 5 is not valid id
            var result = await ratingQuery.Handle(command, cancellationToken);
            Assert.NotNull(result);
            Assert.False(result.Success);

        }

    }
}
