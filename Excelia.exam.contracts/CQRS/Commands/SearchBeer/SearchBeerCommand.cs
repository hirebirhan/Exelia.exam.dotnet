using Excelia.exam.Application.CQRS.DTO;
using MediatR;


namespace Excelia.exam.Application.CQRS.Commands.SearchBeer
{
    public class SearchBeerCommand: IRequest<SearchBeerResponse>
    {
        public SearchBeerCommand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
