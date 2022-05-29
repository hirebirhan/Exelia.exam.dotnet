using Excelia.exam.Application.CQRS.DTO;
using Excelia.exam.contracts.common;
using Exelia.exam.Business.CQRS.Commands.UpdateBeer;
using Exelia.exam.Business.CQRS.DTO;
using Exelia.exam.Business.Helpers;
using Exelia.exam.Data;
using MediatR;

namespace Exelia.exam.Business.CQRS.Queries;

public class AddBeerRatingQuery : IRequestHandler<AddBeerRatingCommand, AddBeerRatingResponse>
{
    private readonly BeerCollectionDbContext _dbContext;

    private readonly AddRateValidator _validator;
    public AddBeerRatingQuery(BeerCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
        _validator = new AddRateValidator(dbContext);
    }

    public async Task<AddBeerRatingResponse> Handle(AddBeerRatingCommand request, CancellationToken cancellationToken)
    {
        AddBeerRatingResponse response = new();
        var validationResult = await _validator.ValidateAsync(request,cancellationToken);
        if(!validationResult.IsValid)
        {
            response.Success = false;
            response.Errors = response.Errors = ValidationErrorHelper.GetErrorMessage(validationResult.Errors);

        }

        var beer = await _dbContext.Beers.FindAsync(new object?[] { request.BeerId }, cancellationToken);
        if(beer == null)
        {
            response.Success = false;
            response.Errors = new List<Error>()
            {
                new Error("Unable to find beer", "Beer Id has to be Valid ") 
            };
            return response;
        }

        var rating = new Rating()
        {
            BeerId = request.BeerId, RatingValue = request.Rating, CreatedDate = DateTimeOffset.Now
        };
        _dbContext.Ratings.Add(rating);
        await _dbContext.SaveChangesAsync(cancellationToken);
        response.Success = true;
        response.Data = new BeerResource()
        {
            Name = beer.Name,
            Id = beer.Id,
            Rating = RatingHelper.CalculateRating(beer.Ratings)
        };
        
        return response;
    }
}