using System.Net;
using Excelia.exam.contracts.common;
using Exelia.exam.Business.CQRS.Commands.UpdateBeer;
using Exelia.exam.Business.CQRS.DTO;
using Exelia.exam.Data;
using MediatR;

namespace Excelia.exam.Application.CQRS.Queries;

public class UpdateBeerRatingQuery: IRequestHandler<UpdateBeerRatingCommand, UpdateBeerRatingResponse>
{
    private readonly BeerCollectionDbContext _dbContext;

    public UpdateBeerRatingQuery(BeerCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<UpdateBeerRatingResponse> Handle(UpdateBeerRatingCommand request, CancellationToken cancellationToken)
    {
        UpdateBeerRatingResponse response = new();
        if (request.Rating is > 5 or < 0)
        {
            response.Success = false;
            response.Errors = new List<Error>()
            {
                new Error()
                {
                    Message = "Invalid rating value", Description = "rating has to be between 1 and 5"
                }
            };
            return response;
        }

        var entity =await _dbContext.Beers.FindAsync(new object?[] { request.Id }, cancellationToken);
        if (entity!=null)
        {
            entity.Rating = request.Rating;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            response.Success = true;
            response.StatusCode= HttpStatusCode.OK;
            response.Data = new DTO.BeerResource(entity.Id, entity.Name, entity.Rating);
            return response;
        }
        if (entity == null)
        {
            response.Success = false;
            response.StatusCode = HttpStatusCode.NotFound;
            response.Errors = new List<Error>()
            {
                new Error()
                {
                    Description = "Entity not found", Message = "unable to find a beer with a given Id"
                }
            };
        }
       
        
        return response;
    }
}