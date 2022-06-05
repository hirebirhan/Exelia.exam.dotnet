using Excelia.exam.Application.CQRS.Commands.GetBeers;
using Excelia.exam.Application.CQRS.Queries;
using Exelia.exam.Data;
using System.Net;
using Xunit;

namespace Exelia.exam.unit.test.Handlers
{
    public class GetBeersQueryTest
    {
        private readonly GetBeersQuery getBeersQuery;
        private readonly FakeDbContext _dbContext;
        public GetBeersQueryTest()
        {
            _dbContext = new FakeDbContext();
            getBeersQuery = new GetBeersQuery(_dbContext);
        }


        [Fact]
        public async Task ShouldGetAllBeers()
        {
            //Add dummy beer records
            var beer = new Beer() { Name="Test beer one" };
            var beer2 = new Beer() { Name="Test beer two" };
            var beer3 = new Beer() { Name="Test beer two" };

            _dbContext.Add(beer);
            _dbContext.Add(beer2);
            _dbContext.Add(beer3);
            CancellationToken cancellation = new();
            await _dbContext.SaveChangesAsync(cancellation);


            var command = new GetBeersCommand(10,1);
            CancellationToken cancellationToken = new();
            var result =await getBeersQuery.Handle(command, cancellationToken);
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
