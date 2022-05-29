using Exelia.exam.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Exelia.exam.Business.CQRS.Commands.UpdateBeer
{
    public class AddRateValidator: AbstractValidator<AddBeerRatingCommand>
    {
        private readonly BeerCollectionDbContext _dbContext;

        public AddRateValidator(BeerCollectionDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(c=>c.BeerId).NotNull().MustAsync(ValidBeerId).WithMessage("Beer Id has to be valid ");
            RuleFor(b=>b.Rating).NotNull().GreaterThan(0).LessThan(6);
        }

        private async Task<bool> ValidBeerId(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Beers.Where(x => x.Id == id).AnyAsync(cancellationToken);
        }
    }
}
