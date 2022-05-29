using Excelia.exam.Application.CQRS.DTO;
using MediatR;

namespace Excelia.exam.Application.CQRS.Commands.CreateBeer;

public class CreateBeerCommand : IRequest<CreateBeerResponse>
{

    public CreateBeerCommand(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}