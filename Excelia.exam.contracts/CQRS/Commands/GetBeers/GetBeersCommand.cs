using Excelia.exam.Application.CQRS.DTO;
using MediatR;

namespace Excelia.exam.Application.CQRS.Commands.GetBeers;

public class GetBeersCommand : IRequest<GetBeersResponse>
{
    public GetBeersCommand(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}