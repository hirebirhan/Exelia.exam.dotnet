using Excelia.exam.contracts.common;

namespace Excelia.exam.Application.CQRS.DTO;

public class GetBeersResponse : ApiResponse<BeerResource>
{
    public new List<BeerResource> Data { get; set; }
    public int TotalCount { get; set; }

}