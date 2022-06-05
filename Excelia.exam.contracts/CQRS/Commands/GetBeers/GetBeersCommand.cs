using Excelia.exam.Application.CQRS.DTO;
using MediatR;

namespace Excelia.exam.Application.CQRS.Commands.GetBeers;

public class GetBeersCommand : IRequest<GetBeersResponse>
{
    public GetBeersCommand(int pageSize, int pageNumber)
    {
        PageNumber = pageNumber>0?pageNumber:1;
        PageSize = pageSize>0?pageSize:10;
    }
    
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}