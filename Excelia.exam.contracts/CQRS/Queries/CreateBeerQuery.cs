using Excelia.exam.Application.CQRS.Commands.CreateBeer;
using Excelia.exam.Application.CQRS.DTO;
using Excelia.exam.contracts.common;
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
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Errors = new List<Error>();
            foreach (var error in validationResult.Errors)
            {
                response.Errors.Add(new Error(error.PropertyName + " " + error.ErrorMessage, error.ErrorMessage));
            }

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
            Rating = beer.Ratings.Average(r=>r.RatingValue)
        };
    return response;
    }
}