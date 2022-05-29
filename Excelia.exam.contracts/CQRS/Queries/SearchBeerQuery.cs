using Excelia.exam.Application.CQRS.Commands.SearchBeer;
using Excelia.exam.Application.CQRS.DTO;
using Exelia.exam.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exelia.exam.Business.CQRS.Queries;

internal class SearchBeerQuery : IRequestHandler<SearchBeerCommand, SearchBeerResponse>
{
    private readonly BeerCollectionDbContext _dbContext;

    public SearchBeerQuery(BeerCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<SearchBeerResponse> Handle(SearchBeerCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        var data = await _dbContext.Beers.Where(b => b.Name.Contains(request.Name)).Select(be => new BeerResource()
        {
            Id = be.Id, Name = be.Name, Rating = be.Ratings.Average(r=>r.RatingValue)
        }).ToListAsync(cancellationToken);
        SearchBeerResponse response = new(data)
        {
            Success = data.Count > 0,
            Data = data,
            StatusCode= System.Net.HttpStatusCode.OK
        };
        return response;
    }
}