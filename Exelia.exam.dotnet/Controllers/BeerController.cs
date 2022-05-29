using Excelia.exam.Application.CQRS.Commands.CreateBeer;
using Excelia.exam.Application.CQRS.Commands.GetBeers;
using Excelia.exam.Application.CQRS.Commands.SearchBeer;
using Excelia.exam.Application.CQRS.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exelia.exam.Api.Controllers
{
    [Route("api/beer")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BeerController(IMediator meditator)
        {
            _mediator = meditator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBeerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateBeerCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, response);

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBeersResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetBeersResponse> GetAll([FromRoute] int PageSize, int PageNumber)
        {
            GetBeersCommand command = new(PageSize, PageNumber);
            return await _mediator.Send(command);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchBeerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<SearchBeerResponse> Search(string name)
        {
            SearchBeerCommand command = new(name);
            return await _mediator.Send(command);
        }
    }
}
