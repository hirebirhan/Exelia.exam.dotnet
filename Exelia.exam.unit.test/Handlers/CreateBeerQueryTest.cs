using Excelia.exam.Application.CQRS.Commands.CreateBeer;
using Excelia.exam.Application.CQRS.DTO;
using Excelia.exam.Application.CQRS.Queries;
using Xunit;

namespace Exelia.exam.unit.test.Handlers
{
    public class CreateBeerQueryTest
    {
        private readonly FakeDbContext fakeDbContext;
        private readonly CreateBeerQuery createBeerQuery;
        public CreateBeerQueryTest()
        {
            fakeDbContext = new FakeDbContext(); 
            createBeerQuery = new CreateBeerQuery(fakeDbContext);
        }
        [Fact]
        public async Task ShouldCreateNewBeerWhenCommandContainsValidObject()
        {
            //prepare 
            var command = new CreateBeerCommand("Beer name");
            var beerResource = new BeerResource(1, "Beer name", 2);

            CancellationToken cancellationToken = new ();
            var response = await createBeerQuery.Handle(command, cancellationToken);
            Assert.True(response.Success);
            Assert.Equal(beerResource.Name, response.Data?.Name);
        }
        
        [Fact]
        public async Task ShouldNotCreateNewBeerWhenCommandContainsNotValidObject()
        {
            //prepare 
            var command = new CreateBeerCommand("");
            CancellationToken cancellationToken = new ();
            var response = await createBeerQuery.Handle(command, cancellationToken);
            Assert.False(response.Success);
            Assert.Null(response.Data);
            Assert.NotNull(response.Errors);

        }
    }
}
