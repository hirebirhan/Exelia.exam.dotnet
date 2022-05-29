using Excelia.exam.Application.CQRS.DTO;
using MediatR;

namespace Excelia.exam.Application.CQRS.Commands.CreateBeer;

public class CreateBeerCommand : IRequest<CreateBeerResponse>
{
    public string Name { get; set; }
    public int Rating { get; set; }
}