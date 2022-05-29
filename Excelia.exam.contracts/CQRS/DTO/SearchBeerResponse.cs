using Excelia.exam.contracts.common;

namespace Excelia.exam.Application.CQRS.DTO;

public class SearchBeerResponse : ApiResponse<BeerResource>
{
    public SearchBeerResponse(List<BeerResource> beers)
    {
        Data = beers;
    }
    public SearchBeerResponse()
    {

    }
    public new List<BeerResource> Data { get; set; }
}