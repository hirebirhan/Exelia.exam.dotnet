using Excelia.exam.Application.CQRS.Commands.CreateBeer;
using Excelia.exam.Application.CQRS.DTO;
using Excelia.exam.contracts.common;
using Exelia.exam.Business.Helpers;
using Exelia.exam.Data;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace Excelia.exam.Application.CQRS.Queries;

public class CreateBeerQuery : IRequestHandler<CreateBeerCommand, CreateBeerResponse>
{
    private readonly BeerCollectionDbContext _dbContext;
    private readonly CreateBeerValidator _validator;


    public CreateBeerQuery(BeerCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
        _validator = new CreateBeerValidator(dbContext);
    }

    public async Task<CreateBeerResponse> Handle(CreateBeerCommand request, CancellationToken cancellationToken)
    {
        CreateBeerResponse response = new();
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Errors = ValidationErrorHelper.GetErrorMessage(validationResult.Errors);
            return response;
        }

        Beer beer = new()
        {
            Name = request.Name,
            CreatedDate = DateTimeOffset.Now
        };
        _dbContext.Beers.Add(beer);
        await _dbContext.SaveChangesAsync(cancellationToken);
        response.Success = true;
        response.Data = new BeerResource()
        {
            Id = beer.Id, Name = beer.Name,
             Rating = RatingHelper.CalculateRating(beer.Ratings)

        };
    return response;
    }
}