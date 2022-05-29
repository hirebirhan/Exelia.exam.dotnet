using Excelia.exam.Application.CQRS.DTO;
using MediatR;

namespace Excelia.exam.Application.CQRS.Commands.GetBeers;

public class GetBeersCommand : IRequest<GetBeersResponse>
{
    public GetBeersCommand(int pageSize, int pageNumber)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 10 ? 10 : pageSize;
    }
  
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}