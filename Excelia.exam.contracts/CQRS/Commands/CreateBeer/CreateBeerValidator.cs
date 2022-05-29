using Exelia.exam.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Excelia.exam.Application.CQRS.Commands.CreateBeer;

public class CreateBeerValidator : AbstractValidator<CreateBeerCommand>
{
    private readonly BeerCollectionDbContext _dbContext;

    public CreateBeerValidator(BeerCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(b => b.Name).NotNull().NotEmpty().MinimumLength(3).MustAsync(UniqueBeerName).WithMessage("Beer name cannot be duplicated");
    }

    private async Task<bool> UniqueBeerName(string beerName, CancellationToken cancellationToken)
    {
        return !await _dbContext.Beers.Where(b => b.Name == beerName).AnyAsync(cancellationToken);
    }
}