using Excelia.exam.Application.CQRS.Commands.SearchBeer;
using Excelia.exam.Application.CQRS.DTO;
using Exelia.exam.Business.Helpers;
using Exelia.exam.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Exelia.exam.Business.CQRS.Queries
{
    public class SearchBeerQuery : IRequestHandler<SearchBeerCommand, SearchBeerResponse>
    {
        private readonly BeerCollectionDbContext _dbContext;

        public SearchBeerQuery(BeerCollectionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SearchBeerResponse> Handle(SearchBeerCommand request, CancellationToken cancellationToken)
        {

            SearchBeerResponse response = new();
            if (request.Name.Trim().Equals(""))
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            var data = await _dbContext.Beers.Include(x => x.Ratings).Where(b => b.Name.Contains(request.Name)).Select(be => new BeerResource()
            {
                Id = be.Id,
                Name = be.Name,
                Rating = RatingHelper.CalculateRating(be.Ratings)

            }).ToListAsync(cancellationToken);
            response = new(data)
            {
                Success = data.Count > 0,
                Data = data,
                StatusCode = data.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound, 
                
            };
            return response;
        }


    }
}