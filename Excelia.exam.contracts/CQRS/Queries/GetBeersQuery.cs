using Excelia.exam.Application.CQRS.Commands.GetBeers;
using Excelia.exam.Application.CQRS.DTO;
using Exelia.exam.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Excelia.exam.Application.CQRS.Queries;

public class GetBeersQuery : IRequestHandler<GetBeersCommand, GetBeersResponse>
{
    private readonly BeerCollectionDbContext _dbContext;

    public GetBeersQuery(BeerCollectionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GetBeersResponse> Handle(GetBeersCommand request, CancellationToken cancellationToken)
    {
        var response = new GetBeersResponse();
        var items = await _dbContext.Beers.Include(x => x.Ratings)
            .Select(b => new BeerResource()
            {
                Id = b.Id,
                Name = b.Name,
                Rating = b.Ratings.Average(r => r.RatingValue)
            })
            .Skip((request.PageNumber-1)*request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        response.Success = true;
        response.Data = items;
        response.TotalCount = items.Count;
        response.StatusCode = HttpStatusCode.OK;
        return response;
    }
}
