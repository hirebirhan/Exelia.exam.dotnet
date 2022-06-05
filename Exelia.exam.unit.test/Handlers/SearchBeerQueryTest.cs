using Excelia.exam.Application.CQRS.Commands.SearchBeer;
using Exelia.exam.Business.CQRS.Queries;
using Exelia.exam.Data;
using Xunit;

namespace Exelia.exam.unit.test.Handlers
{
    public class SearchBeerQueryTest
    {
        private readonly FakeDbContext fakeDbContext;
        private readonly SearchBeerQuery searchBeerQuery;
        public SearchBeerQueryTest()
        {
            fakeDbContext = new FakeDbContext();
            searchBeerQuery = new SearchBeerQuery(fakeDbContext);

        }

        [Fact]
        public async Task ShouldReturnSearchResultsIfBeerNameMatchesAnyRecord()
        {
            //Add dummy record
            CancellationToken cancellationToken = new ();
            DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow;
            var beer = new Beer() { Name = "heinken", CreatedDate = dateTimeOffset };
            fakeDbContext.Beers.Add(beer);
            await fakeDbContext.SaveChangesAsync(cancellationToken);

            SearchBeerCommand command = new ("heinken");
            var result = await searchBeerQuery.Handle(command, cancellationToken);
            

            Assert.True(result.Success);
            Assert.Equal(result.Data.First().Name, beer.Name);

        }


        [Fact]
        public async Task ShouldReturnSearchResultsIfBeerNameIsNotProvided()
        {
            //Add dummy record
            CancellationToken cancellationToken = new();
            fakeDbContext.Beers.Add(new Beer() { Name = "heinken", CreatedDate = DateTimeOffset.Now });
            await fakeDbContext.SaveChangesAsync(cancellationToken);

            SearchBeerCommand command = new ("");
            var result = await searchBeerQuery.Handle(command, cancellationToken);

            Assert.False(result.Success);
        }
    }
}