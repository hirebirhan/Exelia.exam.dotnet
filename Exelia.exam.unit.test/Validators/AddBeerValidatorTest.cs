using Excelia.exam.Application.CQRS.Commands.CreateBeer;
using Exelia.exam.Data;
using Xunit;

namespace Exelia.exam.unit.test.Validators
{
    public class AddBeerValidatorTest
    {
        private readonly FakeDbContext _dbContext;
        private readonly  CreateBeerValidator _validator;

        public AddBeerValidatorTest()
        {
            _dbContext = new FakeDbContext();
             _validator = new CreateBeerValidator(_dbContext);

        }


        [Fact]
        public async Task ShouldNotCreateIfCommandIsNotValid()
        {
            CreateBeerCommand command = new("");
            var result = await _validator.ValidateAsync(command);
            Assert.False(result.IsValid);
            Assert.NotNull(command);

        }

        [Fact]
        public async Task ShouldCreateIfCommandIsValid()
        {
            CreateBeerCommand command = new("Heinken beer");
            var result = await _validator.ValidateAsync(command);
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);

        }

        //TODO move to model test
        [Fact]
        public void ShouldConstructNewBeerEntity()
        {
            DateTimeOffset currentDate = DateTimeOffset.Now;
            Beer beer = new()
            {
                Id = 1,
                Name = "walia",
                CreatedDate = currentDate,
                UpdatedDate = currentDate,
                Ratings = new List<Rating>() {
                 new Rating()
                 {
                     BeerId= 1, Id=1, RatingValue=5, CreatedDate= currentDate
                 }
            }
            };

            Assert.Equal(1, beer.Id);
            Assert.Equal(currentDate, beer.CreatedDate);

        }
    }
}
