﻿using Exelia.exam.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Excelia.exam.Application.CQRS.Commands.CreateBeer;

public class CreateBeerValidator:AbstractValidator<CreateBeerCommand>
{
    private readonly BeerCollectionDbContext _dbContext;

    public CreateBeerValidator(BeerCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(b => b.Name).NotNull().MustAsync(UniqueBeerName).WithMessage("Beer name cannot be duplicated");
        RuleFor(be => be.Rating).NotEmpty().LessThan(6).GreaterThan(0);
    }

    private async Task<bool> UniqueBeerName(string beerName, CancellationToken cancellationToken)
    {
        return !await _dbContext.Beers.Where(b => b.Name == beerName).AnyAsync(cancellationToken);
    }
}